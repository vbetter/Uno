/********************************************
-	    File Name: UIPlayer
-	  Description: 
-	 	   Author: lijing,<979477187@qq.com>
-     Create Date: Created by lijing on #CREATIONDATE#.
-Revision History: 
********************************************/

using UnityEngine;
using System.Collections;

public class UIPlayer : MonoBehaviour {

    [SerializeField]
    UISprite _sp_icon;

    [SerializeField]
    UILabel _label_name;

    [SerializeField]
    UILabel _label_cards;

	// Use this for initialization
	void Start () {
	
	}
	
    public void Init(string playerName,string iconName)
    {
        _label_name.text = playerName;
        _sp_icon.spriteName = iconName;
    }

    public void SetCardsNumb(uint value)
    {
        _label_cards.text = value.ToString();
    }
}
