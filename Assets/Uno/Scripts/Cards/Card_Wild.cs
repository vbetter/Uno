using UnityEngine;
using System.Collections;

public class Card_Wild : Card {

	// Use this for initialization
	void Start () {
	
	}

    public void Init(ENUM_CARD_COLOR color)
    {
        CardColor = color;
        CardType = ENUM_CARD_TYPE.WILD;
        base.Init();
    }

    public override bool CanPlayCard(Card lastCard)
    {
        switch (lastCard.CardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                return true;
                break;
            case ENUM_CARD_TYPE.PASS:
                break;
            case ENUM_CARD_TYPE.FLIP:
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                return true;
                break;
            case ENUM_CARD_TYPE.WILD:
                return true;
                break;
            default:
                break;
        }
        return base.CanPlayCard(lastCard);
    }
}
