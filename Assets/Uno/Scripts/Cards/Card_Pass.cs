using UnityEngine;
using System.Collections;

public class Card_Pass : Card {

	// Use this for initialization
	void Start () {
	
	}

    public void Init(ENUM_CARD_COLOR color)
    {
        CardColor = color;
        CardType = ENUM_CARD_TYPE.PASS;
        base.Init();
    }

    public override bool CanPlayCard(Card lastCard)
    {
        switch (lastCard.CardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                if (CardColor == lastCard.CardColor) return true;
                break;
            case ENUM_CARD_TYPE.PASS:
                if (!lastCard.HasEffect) return true;
                break;
            case ENUM_CARD_TYPE.FLIP:
                if (!lastCard.HasEffect) return true;
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                if (!lastCard.HasEffect && lastCard.CardColor == CardColor) return true;
                break;
            case ENUM_CARD_TYPE.WILD:
                if (!lastCard.HasEffect && lastCard.CardColor == CardColor) return true;
                break;
            default:
                break;
        }
        return base.CanPlayCard(lastCard);
    }
}
