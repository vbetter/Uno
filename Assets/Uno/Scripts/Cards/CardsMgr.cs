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

    List<CardStruct> _openCardList = new List<CardStruct>();//可以摸得牌

    List<CardStruct> _closeCardList = new List<CardStruct>();//打出的牌

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

    public List<CardStruct> OpenCardList
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

    public List<CardStruct> CloseCardList
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
        return getCardList;
    }
}
