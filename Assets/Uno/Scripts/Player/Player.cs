/********************************************
-	    File Name: Player
-	  Description: 
-	 	   Author: lijing,<979477187@qq.com>
-     Create Date: Created by lijing on #CREATIONDATE#.
-Revision History: 
********************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public UIPlayer MyUIPlayer = null;

    [SyncVar]
    public string playerName;

    [SyncVar]
    public Color color;

    public SyncListCardItem HaveCards = new SyncListCardItem();     //手牌
    public SyncListCardItem AddCards = new SyncListCardItem();      //增加卡牌
    public SyncListCardItem PlayCards = new SyncListCardItem();     //打出卡牌
    
    public int IconIndex = 0;//用于头像显示

    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between spaceship & manager is created first).
    protected bool _wasInit = false;

    void Awake()
    {
        NetworkGameMgr._players.Add(this);
    }

	// Use this for initialization
	void Start () {
        
    }

    public void Init()
    {
        if (_wasInit)
            return;

        _wasInit = true;

        if(isLocalPlayer)
        {
            MyUIMain.Init();

            MyUIMain.SetActiveDealBtn(isServer);
        }
    }

    [ClientRpc]
    public void Rpc_Init()
    {
        if (isLocalPlayer)
        {
            MyUIMain.Init();

            MyUIMain.SetActiveDealBtn(isServer);
        }
    }

    [Server]
    public void server_AddCard(List<CardStruct> cards)
    {
        Debug.Log("cards count:" + cards.Count);
        //摸牌
        for (int i = 0; i < cards.Count; i++)
        {
            CardStruct card = cards[i];
            HaveCards.Add(card);
            AddCards.Add(card);
        }

        Rpc_AddCard();
    }

    [ClientRpc]
    public void Rpc_AddCard()
    {
        if(isLocalPlayer)
        {
            Debug.Log("Rpc_AddCard HaveCards.Count:" + HaveCards.Count);
            MyUIMain._UIMyCards.AddCard(AddCards);
            AddCards.Clear();
        }        
    }

    [ClientRpc]
    public void Rpc_SetCardsNumb(uint value)
    {
        MyUIPlayer.SetCardsNumb(value);
    }

    UIMain _UIMain = null;
    
    UIMain MyUIMain
    {
        get
        {
            if(_UIMain==null)
            {
                _UIMain = GameObject.Find("UIMainPanel").GetComponent<UIMain>();
            }

            return _UIMain;
        }
    }

    [Command]
    public void CmdDealCards()
    {
        NetworkGameMgr.Instance.ServerDealCard();
    }

    [Command]
    public void CmdGetCards(uint numb)
    {
        NetworkGameMgr.Instance.ServerGetCards(this,numb);
    }

    [Command]
    public void CmdPlayCards()
    {
        if(PlayCards.Count>0)
        {
            for (int i = 0; i < PlayCards.Count; i++)
            {
                CardStruct card = PlayCards[i];

                for (int j = 0; j < HaveCards.Count; j++)
                {
                    CardStruct playCard = HaveCards[j];
                    if(playCard.UID == card.UID)
                    {
                        HaveCards.RemoveAt(j);
                        NetworkGameMgr.Instance.MyCardsMgr.CloseCardList.Add(playCard);
                    }
                }
            }
            //更新卡牌显示
            Rpc_SetCardsNumb(HaveCards.Count);
            NetworkGameMgr.Instance.MyCardsMgr.Rpc_UpdateCardNumbers();
        }
    }

    public void AddCard_toPlayCards(CardStruct card)
    {
        for (int i = 0; i < PlayCards.Count; i++)
        {
            CardStruct item = PlayCards[i];
            if (item.UID == card.UID)
            {
                return;
            }
        }

        PlayCards.Add(card);

        MyUIMain.SetLabelChooseCards(PlayCards.Count);
    }

    public void RemoveCard_toPlayCards(CardStruct card)
    {
        for (int i = 0; i < PlayCards.Count; i++)
        {
            CardStruct item = PlayCards[i];
            if(item.UID == card.UID)
            {
                
                PlayCards.RemoveAt(i);
                MyUIMain.SetLabelChooseCards(PlayCards.Count);
                return;
            }
        }
    }
}
