/********************************************
-	    File Name: Utils
-	  Description: 
-	 	   Author: lijing,<979477187@qq.com>
-     Create Date: Created by lijing on #CREATIONDATE#.
-Revision History: 
********************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class Utils {

    public static Player ClientLocalPlayer()
    {
        // note: ClientScene.localPlayers.Count cant be used as check because
        // nothing is removed from that list, even after disconnect. It still
        // contains entries like: ID=0 NetworkIdentity NetID=null Player=null
        // (which might be a UNET bug)
        var p = ClientScene.localPlayers.Find(pc => pc.gameObject != null);
        return p != null ? p.gameObject.GetComponent<Player>() : null;
    }

    public static string GetColorChinaNameWithType(ENUM_CARD_COLOR color)
    {
        string colorstring = "";
        switch (color)
        {
            case ENUM_CARD_COLOR.NONE:
                break;
            case ENUM_CARD_COLOR.RED:
                colorstring = "红色";
                break;
            case ENUM_CARD_COLOR.YELLOW:
                colorstring = "黄色";
                break;
            case ENUM_CARD_COLOR.BLUE:
                colorstring = "蓝色";
                break;
            case ENUM_CARD_COLOR.GREEN:
                colorstring = "绿色";
                break;
            default:
                break;
        }
        return colorstring;
    }

    public static string GetColorNameWithType(ENUM_CARD_COLOR color)
    {
        string colorstring = "";
        switch (color)
        {
            case ENUM_CARD_COLOR.NONE:
                break;
            case ENUM_CARD_COLOR.RED:
                colorstring = "red";
                break;
            case ENUM_CARD_COLOR.YELLOW:
                colorstring = "yellow";
                break;
            case ENUM_CARD_COLOR.BLUE:
                colorstring = "blue";
                break;
            case ENUM_CARD_COLOR.GREEN:
                colorstring = "green";
                break;
            default:
                break;
        }
        return colorstring;
    }

    public static string GetCardTypeNameWithType(ENUM_CARD_TYPE type)
    {
        string colorstring = "";
        switch (type)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                colorstring = "numb";
                break;
            case ENUM_CARD_TYPE.STOP:
                colorstring = "stop";
                break;
            case ENUM_CARD_TYPE.FLIP:
                colorstring = "flip";
                break;
            case ENUM_CARD_TYPE.DRAW2:
                colorstring = "draw2";
                break;
            case ENUM_CARD_TYPE.WILD:
                colorstring = "wild";
                break;
            case ENUM_CARD_TYPE.WILD_DRAW4:
                colorstring = "wild_draw4";
                break;
            default:
                break;
        }
        return colorstring;
    }
}
