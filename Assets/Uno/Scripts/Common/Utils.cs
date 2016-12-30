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

    public static string GetCardSpriteName(CardStruct card)
    {
        string spName = "";

        switch ((ENUM_CARD_COLOR)card.CardColor)
        {
            case ENUM_CARD_COLOR.NONE:
                break;
            case ENUM_CARD_COLOR.RED:
                spName = "card_red";
                break;
            case ENUM_CARD_COLOR.YELLOW:
                spName = "card_yellow";
                break;
            case ENUM_CARD_COLOR.BLUE:
                spName = "card_blue";
                break;
            case ENUM_CARD_COLOR.GREEN:
                spName = "card_green";
                break;
            default:
                break;
        }

        ENUM_CARD_TYPE cardType = (ENUM_CARD_TYPE)card.CardType;
        if (cardType == ENUM_CARD_TYPE.DRAWTWO)
        {
            spName = "card_drawtwo";

        }
        else if (cardType == ENUM_CARD_TYPE.WILD)
        {
            spName = "card_wild";
        }
        return spName;
    }

    public static string GetCardNumb(CardStruct card)
    {
        string value = "";

        switch ((ENUM_CARD_TYPE)card.CardType)
        {
            case ENUM_CARD_TYPE.NONE:
                break;
            case ENUM_CARD_TYPE.NUMBER:
                value = card.CardNumber.ToString();
                break;
            case ENUM_CARD_TYPE.PASS:
                value = card.CardName;
                break;
            case ENUM_CARD_TYPE.FLIP:
                value = card.CardName;
                break;
            case ENUM_CARD_TYPE.DRAWTWO:
                break;
            case ENUM_CARD_TYPE.WILD:
                break;
            default:
                break;
        }

        return value;
    }

    public static string GetColorString(ENUM_CARD_COLOR color)
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
}
