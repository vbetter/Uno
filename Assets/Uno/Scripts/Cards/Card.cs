using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

/// <summary>
/// 卡牌颜色
/// </summary>
public enum ENUM_CARD_COLOR
{
    NONE,
    RED,
    YELLOW,
    BLUE,
    GREEN,
}

/// <summary>
/// 卡牌类型
/// </summary>
public enum ENUM_CARD_TYPE
{
    NONE,
    NUMBER,     //数字
    STOP,       //禁止
    FLIP,       //翻转
    DRAW2,      //+2
    WILD,       
    WILD_DRAW4
}

[System.Serializable]
public struct CardStruct
{
    public int CardColor;
    public int CardType;
    public int CardNumber;
    public string CardName;
    public int UID;//卡片唯一id
}

public class SyncListCardItem : SyncListStruct<CardStruct> { }

public class Card
{
    #region 变量

    public CardStruct MyCardStruct = new CardStruct();

    public bool HasEffect = true;//有效果

    #endregion

    static uint UID_Counter = 0;

    public static T Create<T>(ENUM_CARD_TYPE type) where T : Card
    {
        Card t = null;
        switch (type)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                t = new Card_Number();
                break;
            case ENUM_CARD_TYPE.STOP:
                t = new Card_Pass();
                break;
            case ENUM_CARD_TYPE.FLIP:
                t = new Card_Flip();
                break;
            case ENUM_CARD_TYPE.DRAW2:
                t = new Card_DrawTwo();
                break;
            case ENUM_CARD_TYPE.WILD:
                t = new Card_Wild();
                break;
            case ENUM_CARD_TYPE.WILD_DRAW4:
                t = new Card_Wild4();
                break;
            default:
                break;
        }

        return t as T;
    }

    public virtual bool CanPlayCard(Card lastCard)
    {
        return false;
    }

    public virtual void Init(CardStruct value)
    {
        MyCardStruct = value;
    }
}
