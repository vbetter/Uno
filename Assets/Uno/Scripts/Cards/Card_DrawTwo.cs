using UnityEngine;
using System.Collections;

public class Card_DrawTwo : Card {

    public override bool CanPlayCard(Card lastCard)
    {
        ENUM_CARD_TYPE cardType = (ENUM_CARD_TYPE)lastCard.MyCardStruct.CardType;
        switch (cardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                return true;
                break;
            case ENUM_CARD_TYPE.STOP:
                return true;
                break;
            case ENUM_CARD_TYPE.FLIP:
                break;
            case ENUM_CARD_TYPE.DRAW2:
                return true;
                break;
            case ENUM_CARD_TYPE.WILD:
                if (!lastCard.HasEffect) return true;
                break;
            case ENUM_CARD_TYPE.WILD_DRAW4:
                if (!lastCard.HasEffect) return true;
                break;
            default:
                break;
        }

        return base.CanPlayCard(lastCard);
    }
}
