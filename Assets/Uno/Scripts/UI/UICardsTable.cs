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
	
	}

    public void PlayCard(CardStruct card)
    {
        AddCard(card);
    }

    void AddCard(CardStruct card)
    {
        if(_lastUICard == null)
        {
            GameObject go = GameObject.Instantiate(ResUICard) as GameObject;
            go.SetActive(true);
            _container_cards.AddChild(go.transform);
            go.transform.localScale = Vector3.one;
            _lastUICard = go.GetComponent<UICard>();
        }

        _lastUICard.InitCard(card);
    }
}
