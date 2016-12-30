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

    //List<Card> _haveCards = new List<Card>();  //手牌

    public SyncListCardItem HaveCards = new SyncListCardItem();

    void Awake()
    {
        NetworkGameMgr._players.Add(this);
    }

	// Use this for initialization
	void Start () {
        
    }

    public void Init()
    {
        if (isLocalPlayer)
            MyUIMain.Init();
    }
	
    [Server]
    public void server_AddCard(List<CardStruct> cards)
    {
        Debug.Log("1");
        //摸牌
        for (int i = 0; i < cards.Count; i++)
        {
            CardStruct card = cards[i];
            HaveCards.Add(card);
        }
    }

    [ClientRpc]
    public void Rpc_AddCard()
    {
        LocalAddCard();
        
    }

    [Client]
    public void LocalAddCard()
    {
        Debug.Log("LocalAddCard:"+HaveCards.Count);
        //MyUIMain._UIMyCards.AddCard(HaveCards);

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
        if(!isClient)
        NetworkGameMgr.Instance.ServerDealCard();

        Rpc_AddCard();
    }

}
