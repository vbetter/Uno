using UnityEngine;
using System.Collections;

public class UICardsTable : MonoBehaviour {

    [SerializeField]
    UILabel _label;

    [SerializeField]
    UISprite _sprite;

	// Use this for initialization
	void Start () {
	
	}

    public void PlayCard(Card card)
    {
        if (card == null) return;

        _sprite.spriteName = GetCardSpriteName(card);
        _label.text = GetCardNumb(card);
    }

    string GetCardSpriteName(Card card)
    {
        string spName = "";

        switch (card.CardColor)
        {
            case ENUM_CARD_COLOR.NONE:
                break;
            case ENUM_CARD_COLOR.RED:
                spName = "card_red";
                break;
            case ENUM_CARD_COLOR.YELLOW:
                spName = "card_yellow";
                break;
            case ENUM_CARD_COLOR.BLUE:
                spName = "card_blue";
                break;
            case ENUM_CARD_COLOR.GREEN:
                spName = "card_green";
                break;
            default:
                break;
        }

        if (card.CardType == ENUM_CARD_TYPE.DRAWTWO)
        {
            spName = "card_drawtwo";

        }
        else if (card.CardType == ENUM_CARD_TYPE.WILD)
        {
            spName = "card_wild";
        }
        return spName;
    }

    string GetCardNumb(Card card)
    {
        string value = "";

        switch (card.CardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                value = card.CardNumber.ToString();
                break;
            case ENUM_CARD_TYPE.PASS:
                value = card.CardName;
                break;
            case ENUM_CARD_TYPE.FLIP:
                value = card.CardName;
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                break;
            case ENUM_CARD_TYPE.WILD:
                break;
            default:
                break;
        }

        return value;
    }
}
