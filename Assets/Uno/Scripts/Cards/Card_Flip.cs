using UnityEngine;
using System.Collections;

public class Card_Flip : Card {

	// Use this for initialization
	void Start () {
	
	}

    public override bool CanPlayCard(Card lastCard)
    {
        ENUM_CARD_TYPE CardColor = (ENUM_CARD_TYPE)MyCardStruct.CardType;
        ENUM_CARD_TYPE LastCardColor = (ENUM_CARD_TYPE)lastCard.MyCardStruct.CardType;

        switch (CardColor)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                if (CardColor == LastCardColor) return true;
                break;
            case ENUM_CARD_TYPE.STOP:
                break;
            case ENUM_CARD_TYPE.FLIP:
                break;
            case ENUM_CARD_TYPE.DRAW2:
                if (!lastCard.HasEffect && LastCardColor == CardColor) return true;
                break;
            case ENUM_CARD_TYPE.WILD:
                if (!lastCard.HasEffect && LastCardColor == CardColor) return true;
                break;
            case ENUM_CARD_TYPE.WILD_DRAW4:
                if (!lastCard.HasEffect && LastCardColor == CardColor) return true;
                break;
            default:
                break;
        }

        return base.CanPlayCard(lastCard);
    }
}
