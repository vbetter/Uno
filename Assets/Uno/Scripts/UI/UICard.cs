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

    bool isChoose = false;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Init(CardStruct card)
    {
        UID = card.UID;
        _bg.spriteName = Utils.GetCardSpriteName(card);
        _label.text = Utils.GetCardNumb(card);
        _sp_choose.gameObject.SetActive(isChoose);

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
}
