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
    [SyncVar]
    public string playerName;

    [SyncVar]
    public Color color;

    public SyncListCardItem HaveCards = new SyncListCardItem();//手牌

    public int IconIndex = 0;//用于头像显示

    void Awake()
    {
        NetworkGameMgr._players.Add(this);
    }

	// Use this for initialization
	void Start () {
        Init();
    }

    [Client]
    public void Init()
    {
        if (isLocalPlayer)
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
        }

        Rpc_AddCard();
    }

    [ClientRpc]
    public void Rpc_AddCard()
    {
        if(isLocalPlayer)
        {
            Debug.Log("Rpc_AddCard HaveCards.Count:" + HaveCards.Count);
            MyUIMain._UIMyCards.AddCard(HaveCards);
        }        
    }

    [Command]
    public void Cmd_PlayCard(List<Card> cards)
    {
        //出牌

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

}
