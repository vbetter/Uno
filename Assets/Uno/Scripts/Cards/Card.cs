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
    NUMBER,
    PASS,
    FLIP,
    DRAWTWO,
    WILD
}

[System.Serializable]
public struct CardStruct
{
    public int CardColor;
    public int CardType;
    public uint CardNumber;
    public string CardName;
    public uint UID;//卡片唯一id
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
            case ENUM_CARD_TYPE.PASS:
                t = new Card_Pass();
                break;
            case ENUM_CARD_TYPE.FLIP:
                t = new Card_Flip();
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                t = new Card_DrawTwo();
                break;
            case ENUM_CARD_TYPE.WILD:
                t = new Card_Wild();
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

    /*
    public string CardName;
    public void InitCardName(ref string cardName)
    {
        switch (CardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                cardName = string.Format("{0} {1}",GetColorString(CardColor),CardNumber);
                break;
            case ENUM_CARD_TYPE.PASS:
                cardName = string.Format("{0} PASS", GetColorString(CardColor));
                break;
            case ENUM_CARD_TYPE.FLIP:
                cardName = string.Format("{0} FLIP", GetColorString(CardColor));
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                cardName = string.Format("DRAWTWO");
                break;
            case ENUM_CARD_TYPE.WILD:
                cardName = string.Format("WILD" );
                break;
            default:
                break;
        }
    }

     */
}
