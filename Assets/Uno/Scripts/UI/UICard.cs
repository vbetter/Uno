using UnityEngine;
using System.Collections;

public class UICard : MonoBehaviour {
    [SerializeField]
    UISprite _bg;

    [SerializeField]
    UILabel _label;

    public uint UID = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Init(CardStruct card)
    {
        UID = card.UID;
        _bg.spriteName = Utils.GetCardSpriteName(card);
        _label.text = Utils.GetCardNumb(card);
    }
}
