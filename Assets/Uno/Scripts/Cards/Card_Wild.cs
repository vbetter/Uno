﻿using UnityEngine;
using System.Collections;

public class Card_Wild : Card {

	// Use this for initialization
	void Start () {
	
	}

    public override bool CanPlayCard(Card lastCard)
    {

        return base.CanPlayCard(lastCard);
    }
}
