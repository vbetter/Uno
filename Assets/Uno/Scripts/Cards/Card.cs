using UnityEngine;
using System.Collections;

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

public class Card
{
    #region 变量

    public ENUM_CARD_COLOR CardColor = ENUM_CARD_COLOR.NONE;
    public ENUM_CARD_TYPE CardType = ENUM_CARD_TYPE.NONE;
    public uint CardNumber;
    public bool HasEffect = true;//有效果
    public uint UID;//卡片唯一id

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

    public void Init()
    {
        UID = UID_Counter;
        UID_Counter++;

        InitCardName(ref CardName);
    }

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

    public static string GetColorString(ENUM_CARD_COLOR color)
    {
        string colorstring = "";
        switch (color)
        {
            case ENUM_CARD_COLOR.NONE:
                break;
            case ENUM_CARD_COLOR.RED:
                colorstring = "红色";
                break;
            case ENUM_CARD_COLOR.YELLOW:
                colorstring = "黄色";
                break;
            case ENUM_CARD_COLOR.BLUE:
                colorstring = "蓝色";
                break;
            case ENUM_CARD_COLOR.GREEN:
                colorstring = "绿色";
                break;
            default:
                break;
        }
        return colorstring;
    }
}
