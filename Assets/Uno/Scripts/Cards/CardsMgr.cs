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
        for (int j = 1; j <= CardsMgr.MaxColorNumber; j++)
        {
            ENUM_CARD_COLOR myColor = (ENUM_CARD_COLOR)j;

            //每种颜色2张
            for (int s = 0; s < 2; s++)
            {
                //创建普通牌
                for (uint i = 0; i < 10; i++)
                {
                    CardStruct card = new CardStruct();
                    card.CardNumber = i;
                    card.CardType = (int)ENUM_CARD_TYPE.NUMBER;
                    OpenCardList.Add(card);
                }

                //创建跳过牌
                {
                    CardStruct card = new CardStruct();
                    card.CardType = (int)ENUM_CARD_TYPE.PASS;
                    OpenCardList.Add(card);
                }

                //创建翻转牌
                {
                    CardStruct card = new CardStruct();
                    card.CardType = (int)ENUM_CARD_TYPE.FLIP;
                    OpenCardList.Add(card);
                }

                //创建drawtwo
                {
                    CardStruct card = new CardStruct();
                    card.CardType = (int)ENUM_CARD_TYPE.DRAWTWO;
                    OpenCardList.Add(card);
                }
            }

            //创建万能牌，总共4张
            {
                CardStruct card = new CardStruct();
                card.CardType = (int)ENUM_CARD_TYPE.WILD;
                OpenCardList.Add(card);
            }
        }

        //洗牌
        for (int i = (int)CardsMgr.MaxCardNumb - 1; i >= 1; i--)
        {
            mySwap(i, (int)Random.Range(0, 100000) % ((int)CardsMgr.MaxCardNumb - i), OpenCardList);
        }
    }

    void mySwap(int x, int y, List<CardStruct> list)
    {
        var temp = list[x];
        list[x] = list[y];
        list[y] = temp;
    }
    
    [Server]
    public void SupplementCards()
    {
        for (int i = CloseCardList.Count - 1; i >= 0; i--)
        {
            CardStruct card = CloseCardList[i];
            CloseCardList.Remove(card);
            OpenCardList.Add(card);
        }
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
