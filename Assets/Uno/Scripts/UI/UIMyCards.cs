using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMyCards : MonoBehaviour {

    [SerializeField]
    UIScrollView _scrollView;

    [SerializeField]
    UIGrid _grid;

    List<UICard> _cardsList = new List<UICard>();

    [SerializeField]
    GameObject ResUICard;
	// Use this for initialization
	void Start () {
	
	}

    public void AddCard(CardStruct card)
    {
        GameObject go = GameObject.Instantiate(ResUICard) as GameObject;

        _grid.AddChild(go.transform);
        go.transform.localScale = Vector3.one;
        UICard uicard = go.GetComponent<UICard>();
        uicard.Init(card);
        _cardsList.Add(uicard);

        _grid.Reposition();
    }

    public void AddCard(SyncListCardItem cardList)
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            CardStruct card = cardList[i];
            AddCard(card);
        }
    }

    public void RemoveCard(CardStruct card)
    {
        UICard uicard = _cardsList.Find(a => a.UID == card.UID);
        if(uicard!=null)
        {
            _cardsList.Remove(uicard);
            Destroy(uicard.gameObject);
            _grid.Reposition();
        }
    }

}
