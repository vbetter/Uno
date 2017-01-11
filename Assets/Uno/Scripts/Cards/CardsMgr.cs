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
    public CardStruct LastCard;

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
        CardStruct firstCard = GetCard(true);
        LastCard = firstCard;
        LastCard.HasEffect = false;
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

    [Server]
    public CardStruct GetCard(bool isWild = false)
    {
        int randIndex = UnityEngine.Random.Range(0, OpenCardList.Count - 1);

        CardStruct card = OpenCardList[randIndex];
        if(isWild)
        {
            if ((ENUM_CARD_TYPE)card.CardType == ENUM_CARD_TYPE.WILD_DRAW4 || (ENUM_CARD_TYPE)card.CardType == ENUM_CARD_TYPE.WILD)
            {
                return GetCard(isWild);
            }
        }

        OpenCardList.Remove(card);
        CloseCardList.Add(card);

        Rpc_UpdateCardNumbers();
        return card;
    }

    [Server]
    public void PlayCard(CardStruct playCard)
    {
        
        CloseCardList.Add(playCard);

        if(LastCard.HasEffect )
        {
            switch ((ENUM_CARD_TYPE)LastCard.CardType)
            {
                case ENUM_CARD_TYPE.NONE:
                    break;
                case ENUM_CARD_TYPE.NUMBER:
                    break;
                case ENUM_CARD_TYPE.STOP:
                    NetworkGameMgr.Instance.UpdateCurPlayerIndex();
                    break;
                case ENUM_CARD_TYPE.FLIP:
                    NetworkGameMgr.Instance.IsClockWise = !NetworkGameMgr.Instance.IsClockWise;
                    break;
                case ENUM_CARD_TYPE.DRAW2:
                    {
                        //禁止下家出牌
                        NetworkGameMgr.Instance.UpdateCurPlayerIndex();
                        //下家摸2张牌
                        NetworkGameMgr.Instance.NextPlayerGetCards(2);
                    }
                    break;
                case ENUM_CARD_TYPE.WILD:
                    {
                        //变色
                    }
                    break;
                case ENUM_CARD_TYPE.WILD_DRAW4:
                    {
                        //禁止下家出牌
                        NetworkGameMgr.Instance.UpdateCurPlayerIndex();
                        //下家摸4张牌
                        NetworkGameMgr.Instance.NextPlayerGetCards(4);
                    }
                    break;
                default:
                    break;
            }
        }

        //更新上一张牌
        LastCard = playCard;
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
