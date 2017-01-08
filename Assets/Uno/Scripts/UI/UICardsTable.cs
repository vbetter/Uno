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

    public void PlayCard(CardStruct card)
    {
        //_sprite.spriteName = Utils.GetCardSpriteName(card);
        //_label.text = Utils.GetCardNumb(card);
    }
}
