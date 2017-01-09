using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public enum ENUM_COMMAND
    {
        NONE,
        Init,//初始化牌库
        SHUFFLE,//发牌
        DEAL,//洗牌
        SUPPLEMENT,//补牌，当牌发完，但是游戏没结束，可以把打出的牌洗牌后补充到牌堆中
    }

    /// <summary>
    /// 命令
    /// </summary>
    public class Command
    {
        protected CardsMgr _cardsMgr;

        public Command(CardsMgr cardsMgr)
        {
            _cardsMgr = cardsMgr;
        }
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <returns></returns>
        public virtual bool Execute() { return true; }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <returns></returns>
        public virtual bool Undo() { return true; }
    }

    #region 发牌命令

    public class DealCommand : Command
    {
        Player _player;
        uint _getNumb;

        public DealCommand(CardsMgr cardsMgr, Player player, uint getNumb)
            : base(cardsMgr)
        {
            _player = player;
            _getNumb = getNumb;
        }

        public override bool Execute()
        {
            if(_player == null)
            {
                return false;
            }

            //List<Card> cardList = _cardsMgr.GetCards(_getNumb);
            //_player.Cmd_AddCard(cardList);
            return base.Execute();
        }
    }

#endregion

#region 洗牌

public class ShuffleCommand : Command
{
    public ShuffleCommand(CardsMgr cardsMgr)
        : base(cardsMgr)
    {

    }

    public override bool Execute()
    {
        SyncListCardItem cardList = _cardsMgr.OpenCardList;
        for (int i = (int)cardList.Count - 1; i >= 1; i--)
        {
            mySwap(i, (int)Random.Range(0, 100000) % ((int)cardList.Count - i), ref cardList);
        }

        return base.Execute();
    }

    void mySwap(int x, int y,ref SyncListCardItem list)
    {
        var temp = list[x];
        list[x] = list[y];
        list[y] = temp;
    }
}


#endregion


#region 补牌，当牌发完，但是游戏没结束，可以把打出的牌洗牌后补充到牌堆中

public class SupplementCommand : Command
    {
        public SupplementCommand(CardsMgr cardsMgr) : base(cardsMgr)
        {

        }

        public override bool Execute()
        {
            for (int i = _cardsMgr.CloseCardList.Count - 1; i >= 0; i--)
            {
                CardStruct card = _cardsMgr.CloseCardList[i];
                _cardsMgr.CloseCardList.Remove(card);
                _cardsMgr.OpenCardList.Add(card);
            }

            return base.Execute();
        }
    }

#endregion

#region 出牌

public class PlayCardCommand:Command
{
    public PlayCardCommand(Card play_card, CardsMgr cardsMgr)
        : base(cardsMgr)
    {

    }

    public override bool Execute()
    {

        return base.Execute();
    }
}

#endregion

#region 初始化牌库

public class InitCardCommand : Command
{
    public InitCardCommand(CardsMgr cardsMgr)
        : base(cardsMgr)
    {

    }

    public override bool Execute()
    {
        SyncListCardItem OpenCardList = _cardsMgr.OpenCardList;

        for (int j = 1; j <= CardsMgr.MaxColorNumber; j++)
        {
            ENUM_CARD_COLOR myColor = (ENUM_CARD_COLOR)j;

            //0数字牌1张
            {
                CardStruct card = new CardStruct();
                card.CardNumber = 0;
                card.CardColor = j;
                card.CardType = (int)ENUM_CARD_TYPE.NUMBER;
                OpenCardList.Add(card);
            }

            //每种颜色2张
            for (int s = 0; s < 2; s++)
            {
                //创建普通牌 1-9
                for (int i = 1; i < 10; i++)
                {
                    CardStruct card = new CardStruct();
                    card.CardNumber = i;
                    card.CardColor = j;
                    card.CardType = (int)ENUM_CARD_TYPE.NUMBER;
                    OpenCardList.Add(card);
                }

                //创建禁止牌
                {
                    CardStruct card = new CardStruct();
                    card.CardType = (int)ENUM_CARD_TYPE.STOP;
                    card.CardColor = j;
                    OpenCardList.Add(card);
                }

                //创建翻转牌
                {
                    CardStruct card = new CardStruct();
                    card.CardType = (int)ENUM_CARD_TYPE.FLIP;
                    card.CardColor = j;
                    OpenCardList.Add(card);
                }

                //创建+2牌
                {
                    CardStruct card = new CardStruct();
                    card.CardType = (int)ENUM_CARD_TYPE.DRAW2;
                    card.CardColor = j;
                    OpenCardList.Add(card);
                }
            }

            //创建wild,总共4张
            {
                CardStruct card = new CardStruct();
                card.CardType = (int)ENUM_CARD_TYPE.WILD;
                OpenCardList.Add(card);
            }

            //创建wild draw4，总共4张
            {
                CardStruct card = new CardStruct();
                card.CardType = (int)ENUM_CARD_TYPE.WILD_DRAW4;
                OpenCardList.Add(card);
            }
        }

        for (int j = 0; j < OpenCardList.Count; j++)
        {
            CardStruct item = OpenCardList[j];
            item.UID = j;
            item.HasEffect = true;
            OpenCardList[j] = item;
        }

        return base.Execute();
    }
}

#endregion