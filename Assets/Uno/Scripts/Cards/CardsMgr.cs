/********************************************
-	    File Name: CardMgr
-	  Description: 
-	 	   Author: lijing,<979477187@qq.com>
-     Create Date: Created by lijing on #CREATIONDATE#.
-Revision History: 
********************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CardsMgr : NetworkBehaviour 
{

    SyncListCardItem _openCardList = new SyncListCardItem();//可以摸得牌

    SyncListCardItem _closeCardList = new SyncListCardItem();//打出的牌

    public static readonly uint MaxColorNumber = 4;         //4种颜色
    public static readonly uint MaxCardNumb = 108;          //总共108张
    public static readonly uint INIT_HAVE_CARDS_NUMB = 7;   //初始手牌7张

    /// <summary>
    /// 上一张牌 
    /// </summary>
    public CardStruct LastCard
    {
        set;get;
    }

    public SyncListCardItem OpenCardList
    {
        set
        {
            _openCardList = value;
        }
        get
        {
            return _openCardList;
        }
    }

    public SyncListCardItem CloseCardList
    {
        get
        {
            return _closeCardList;
        }
    }

	// Use this for initialization
	void Start () {
	
	}

    [Server]    
    public void Init()
    {
        InitCardCommand initCardCommand = new InitCardCommand(this);
        initCardCommand.Execute();

        //洗牌
        ShuffleCommand ShuffleCommand = new ShuffleCommand(this);
        ShuffleCommand.Execute();
    }

    public void CreateFirstCard()
    {
        //创建首牌
        CardStruct firstCard = GetCards(1)[0];
        LastCard = firstCard;
        //牌桌上显示第一张牌
        Rpc_UpdateCardToTable();
    }

    /// <summary>
    /// 获取卡牌
    /// </summary>
    /// <param name="numb"></param>
    /// <returns></returns>
    [Server]
    public List<CardStruct> GetCards(uint numb)
    {
        List<CardStruct> getCardList = new List<CardStruct>();

        for (int i = 0; i < numb; i++)
        {
            CardStruct card = OpenCardList[OpenCardList.Count - 1];
            OpenCardList.Remove(card);
            CloseCardList.Add(card);
            getCardList.Add(card);
        }
        Rpc_UpdateCardNumbers();
        return getCardList;
    }

    [ClientRpc]
    public void Rpc_UpdateCardNumbers()
    {
        NetworkGameMgr.Instance.MyUIMain.UpdateCardsNumber(OpenCardList.Count, CloseCardList.Count);
    }

    [ClientRpc]
    public void Rpc_UpdateCardToTable()
    {
        NetworkGameMgr.Instance.MyUIMain._UICardsTable.PlayCard(LastCard);
    }
}
