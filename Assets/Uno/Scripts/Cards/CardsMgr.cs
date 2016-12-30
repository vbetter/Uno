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

public class CardsMgr : NetworkBehaviour {

    List<Card> _openCardList = new List<Card>();//可以摸得牌

    List<Card> _closeCardList = new List<Card>();//打出的牌

    public static readonly uint MaxColorNumber = 4;     //4种颜色
    public static readonly uint MaxCardNumb = 108;      //总共108张

    Card _lastCard = null;
    public Card LastCard
    {
        set
        {
            _lastCard = value;
        }
        get
        {
            return _lastCard;
        }
    }

    public List<Card> OpenCardList
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

    public List<Card> CloseCardList
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
    }
    
    [Server]
    public void SupplementCards()
    {
        SupplementCommand supplementCommand = new SupplementCommand(this);
        supplementCommand.Execute();
    }

    /// <summary>
    /// 获取卡牌
    /// </summary>
    /// <param name="numb"></param>
    /// <returns></returns>
    [Server]
    public List<Card> GetCards(uint numb)
    {
        List<Card> getCardList = new List<Card>();

        for (int i = 0; i < numb; i++)
        {
            Card card = OpenCardList[OpenCardList.Count - 1];
            OpenCardList.Remove(card);
            CloseCardList.Add(card);
            getCardList.Add(card);
        }
        return getCardList;
    }
}
