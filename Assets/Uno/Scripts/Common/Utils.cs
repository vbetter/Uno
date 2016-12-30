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
	
}
