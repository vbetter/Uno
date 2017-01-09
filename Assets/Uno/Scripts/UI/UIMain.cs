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

    [SerializeField]
    UILabel _label_chooseCards;

    [SerializeField]
    UILabel _label_allCards,_label_removeCards;//牌库总数，打出牌总数

	// Use this for initialization
	void Start () {
	
	}

    public void Init()
    {
        UIEventListener.Get(_btn_playCard.gameObject).onClick = (go) => 
        {
            Debug.Log("_btn_playCard");

            if(Utils.IsPlayWithCards(Utils.ClientLocalPlayer().PlayCards,NetworkGameMgr.Instance.MyCardsMgr.LastCard, Utils.ClientLocalPlayer().IsLastOne))
            {
                for (int i = 0; i < Utils.ClientLocalPlayer().PlayCards.Count; i++)
                {
                    _UIMyCards.RemoveCard(Utils.ClientLocalPlayer().PlayCards[i]);
                }

                Utils.ClientLocalPlayer().CmdPlayCards();
                SetLabelChooseCards(0);
            }
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
            _btn_deal.gameObject.SetActive(false);
        };
    }

    public void UpdateCardsNumber(int hadCards,int removeCards)
    {
        _label_allCards.text = "余牌总数:" + hadCards.ToString();
        _label_removeCards.text = "出牌总数:" + removeCards.ToString();
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

            player.MyUIPlayer = uiplayer;
        }

        return go;
    }

    public void SetActiveDealBtn(bool value)
    {
        _btn_deal.SetActive(value);
    }

    public void SetLabelChooseCards(int value)
    {
        _label_chooseCards.text = "选中卡牌:" + value.ToString();
    }
}
