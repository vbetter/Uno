using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class UIMain : MonoBehaviour {

    public UICardsTable _UICardsTable;
    public UIMyCards _UIMyCards;

    [SerializeField]
    GameObject _btn_playCard , _btn_getCard,_btn_deal;

    [SerializeField]
    public UIPlayer _UIPlayer;

    [SerializeField]
    public UIGrid _playerGrid;


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
            Utils.ClientLocalPlayer().CmdGetCards(1);
        };

        UIEventListener.Get(_btn_deal.gameObject).onClick = (go) =>
        {
            //Debug.Log("_btn_deal");
            Utils.ClientLocalPlayer().CmdDealCards();
        };
    }

    public GameObject InitUIPlayer(Player player)
    {
        GameObject go = GameObject.Instantiate(_UIPlayer.gameObject) as GameObject;
        if(go!=null)
        {
            go.SetActive(true);
            _playerGrid.AddChild(go.transform);
            go.transform.localScale = Vector3.one;

            UIPlayer uiplayer = go.GetComponent<UIPlayer>();
            uiplayer.Init(player.playerName, player.IconIndex.ToString());
            _playerGrid.Reposition();
        }

        return go;
    }

    public void SetActiveDealBtn(bool value)
    {
        _btn_deal.SetActive(value);
    }
}
