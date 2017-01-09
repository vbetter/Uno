using UnityEngine;
using System.Collections;

public class UICard : MonoBehaviour {
    [SerializeField]
    UISprite _bg;

    [SerializeField]
    UILabel _label;

    [SerializeField]
    UISprite _sp_choose;

    public int UID = 0;

    [SerializeField]
    bool isChoose = false;

    [SerializeField]
    int _cardNumb;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Init(CardStruct card)
    {
        InitCard(card);

        UIEventListener.Get(_bg.gameObject).onClick = (go) =>
        {
            if(isChoose)
            {
                Utils.ClientLocalPlayer().RemoveCard_toPlayCards(card);
                isChoose = !isChoose;
            }
            else
            {
                Utils.ClientLocalPlayer().AddCard_toPlayCards(card);
                isChoose = !isChoose;
            }
            _sp_choose.gameObject.SetActive(isChoose);
        };
    }

    public void InitCard(CardStruct card)
    {
        UID = card.UID;
        _cardNumb = card.CardNumber;

        SetCardSprite(card);

        if ((ENUM_CARD_TYPE)card.CardType == ENUM_CARD_TYPE.NUMBER) _label.text = card.CardNumber.ToString();
        else { _label.text = ""; }

        _sp_choose.gameObject.SetActive(isChoose);
    }

    void SetCardSprite(CardStruct card)
    {
        string spName = "";

        ENUM_CARD_TYPE cardType = (ENUM_CARD_TYPE)card.CardType;
        ENUM_CARD_COLOR cardColor = (ENUM_CARD_COLOR)card.CardColor;

        switch (cardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
            case ENUM_CARD_TYPE.STOP:
            case ENUM_CARD_TYPE.FLIP:
            case ENUM_CARD_TYPE.DRAW2:
                spName = Utils.GetCardTypeNameWithType(cardType) + "_" + Utils.GetColorNameWithType(cardColor);
                break;
            case ENUM_CARD_TYPE.WILD:
            case ENUM_CARD_TYPE.WILD_DRAW4:
                spName = Utils.GetCardTypeNameWithType(cardType);
                break;
            default:
                break;
        }
        _bg.spriteName = spName;
    }
}
