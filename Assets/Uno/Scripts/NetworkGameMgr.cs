/********************************************
-	    File Name: NetworkGameMgr
-	  Description: 
-	 	   Author: lijing,<979477187@qq.com>
-     Create Date: Created by lijing on #CREATIONDATE#.
-Revision History: 
********************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkGameMgr : NetworkBehaviour
{
    public static readonly uint INIT_HAVE_CARDS_NUMB = 7;//初始手牌7张

    public static List<Player> _players = new List<Player>();

    [SerializeField]
    CardsMgr _cardsMgr;

    [SerializeField]
    UIMain _uimain;

    static NetworkGameMgr m_Instance = null;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
    public static NetworkGameMgr Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = GameObject.FindObjectOfType(typeof(NetworkGameMgr)) as NetworkGameMgr;
                if (m_Instance == null)
                {
                    m_Instance = new GameObject("Singleton of " + typeof(NetworkGameMgr).ToString(), typeof(NetworkGameMgr)).GetComponent<NetworkGameMgr>();
                    m_Instance.Init();
                }

            }
            return m_Instance;
        }
    }

	// Use this for initialization
	void Start () 
    {
        Init();
	}

    void Init()
    {
        if(isServer)
        {
            InitWithServer();

            Debug.Log("InitWithServer");
        }

        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];
            player.Init();
        }
        //Debug.Log("card count:" + _players[0].HaveCards.Count);
        //Debug.Log("player count : " + _players.Count);
    }

    void InitWithServer()
    {
        _cardsMgr.Init();
    }

    [Server]
    public void ServerDealCard()
    {
        Debug.Log("_players: " + _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];

            List<CardStruct> cardList = _cardsMgr.GetCards(INIT_HAVE_CARDS_NUMB);
            player.server_AddCard(cardList);
        }
    }
}
