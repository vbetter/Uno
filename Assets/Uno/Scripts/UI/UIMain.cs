using UnityEngine;
using System.Collections;

public class UIMain : MonoBehaviour {

    public UICardsTable _UICardsTable;
    public UIMyCards _UIMyCards;

    [SerializeField]
    GameObject _btn_playCard , _btn_getCard,_btn_deal;

    [SerializeField]
    UIPlayer _UIPlayer;


	// Use this for initialization
	void Start () {
	
	}

    public void Init()
    {
        UIEventListener.Get(_btn_playCard.gameObject).onClick = (go) => 
        {
            Debug.Log("_btn_playCard");
        };

        UIEventListener.Get(_btn_getCard.gameObject).onClick = (go) => 
        {
            Debug.Log("_btn_getCard");
        };

        UIEventListener.Get(_btn_deal.gameObject).onClick = (go) =>
        {
            Debug.Log("_btn_deal");
            Utils.ClientLocalPlayer().CmdDealCards();
        };
    }

    public void SetActiveDealBtn(bool value)
    {
        _btn_deal.SetActive(value);
    }
}
