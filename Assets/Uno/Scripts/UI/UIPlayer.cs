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

	// Use this for initialization
	void Start () {
	
	}
	
    public void Init(string playerName,string iconName)
    {
        _label_name.text = playerName;
        _sp_icon.spriteName = iconName;
    }
}
