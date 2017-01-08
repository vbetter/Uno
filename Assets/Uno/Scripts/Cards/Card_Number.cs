using UnityEngine;
using System.Collections;

public class Card_Number : Card {

	// Use this for initialization
	void Start () {
	
	}
	

    public override bool CanPlayCard(Card lastCard)
    {
        ENUM_CARD_COLOR CardColor = (ENUM_CARD_COLOR)MyCardStruct.CardColor;
        ENUM_CARD_COLOR LastCardColor = (ENUM_CARD_COLOR)lastCard.MyCardStruct.CardColor;
        
        /*
        ENUM_CARD_TYPE cardType = (ENUM_CARD_TYPE)
        int CardNumber = MyCardStruct.CardNumber;
        int LastCardNumber = lastCard.MyCardStruct.CardNumber;

        switch (card)
        {
            default:
                break;
        }
        switch ((ENUM_CARD_TYPE)lastCard.MyCardStruct.CardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                if (CardNumber == LastCardNumber || CardColor == LastCardColor) return true;
                break;
            case ENUM_CARD_TYPE.PASS:
                if (!lastCard.HasEffect) return true;
                break;
            case ENUM_CARD_TYPE.FLIP:
                if (!lastCard.HasEffect) return true;
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                if (!lastCard.HasEffect && LastCardColor == CardColor) return true;
                break;
            case ENUM_CARD_TYPE.WILD:
                if (!lastCard.HasEffect && LastCardColor == CardColor) return true;
                break;
            default:
                break;
        }
        */
        return base.CanPlayCard(lastCard);
    }
}
