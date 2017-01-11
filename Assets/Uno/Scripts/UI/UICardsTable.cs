using UnityEngine;
using System.Collections;

public class UICardsTable : MonoBehaviour {

    [SerializeField]
    GameObject _container_cards;

    [SerializeField]
    UICard _lastUICard;

    [SerializeField]
    GameObject ResUICard;

	// Use this for initialization
	void Start () {
	    if(_lastUICard == null)
        {
            GameObject go = GameObject.Instantiate(ResUICard) as GameObject;
            go.SetActive(true);
            go.transform.parent = _container_cards.transform;
            go.transform.localScale = Vector3.one;
            _lastUICard = go.GetComponent<UICard>();

        }
	}

    public void PlayCard(CardStruct card)
    {
        AddCard(card);
    }

    void AddCard(CardStruct card)
    {
        _lastUICard.InitCard(card);
    }
}
