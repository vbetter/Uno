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
    public static List<Player> _players = new List<Player>();

    [SyncVar]
    public bool IsClockWise = true;             //顺时针旋转出牌

    [SyncVar]
    public int CurPlayerIndex = 0;              //当前行动玩家索引

    [SerializeField]
    CardsMgr _cardsMgr;

    [SerializeField]
    UIMain _uimain;

    static NetworkGameMgr m_Instance = null;

    public static int IconIndex = 0;            //用来显示玩家头像的索引

    public CardsMgr MyCardsMgr
    {
        get
        {
            return _cardsMgr;
        }
    }

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
        }

        if (isLocalPlayer)
        {
            MyUIMain.Init();

            MyUIMain.SetActiveDealBtn(isServer);
        }

        InitUIPlayers();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

    }

    public override void OnStartServer()
    {
        base.OnStartServer();

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }


    void InitUIPlayers()
    {
        Debug.Log("NetworkGameMgr._players.Count ： " + _players.Count);

        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];
            player.Init();
        }
    }

    void InitPlayerWithServer()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            Player player = _players[i];

            if (NetworkServer.active)
                NetworkServer.Spawn(MyUIMain.InitUIPlayer(player));
        }
    }

    UIMain _UIMain = null;
    public UIMain MyUIMain
    {
        get
        {
            if (_UIMain == null)
            {
                _UIMain = GameObject.Find("UIMainPanel").GetComponent<UIMain>();
            }

            return _UIMain;
        }
    }

    [Server]
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

            List<CardStruct> cardList = _cardsMgr.GetCards(CardsMgr.INIT_HAVE_CARDS_NUMB);
            player.server_AddCard(cardList);

            player.Rpc_SetCardsNumb(player.HaveCards.Count);
        }

        _cardsMgr.CreateFirstCard();
    }

    [Server]
    public void ServerGetCards(Player player,uint value)
    {
        List<CardStruct> cardList = _cardsMgr.GetCards(value);
        player.server_AddCard(cardList);
        player.Rpc_SetCardsNumb(player.HaveCards.Count);
    }

    [Server]
    public void NextPlayerGetCards(uint value)
    {
        Player player = GetNextPlayer();
        List<CardStruct> cardList = _cardsMgr.GetCards(value);
        player.server_AddCard(cardList);
        player.Rpc_SetCardsNumb(player.HaveCards.Count);
    }

    [Server]
    public void PlayCard(CardStruct card)
    {
        MyCardsMgr.PlayCard(card);
    }

    public Player GetLastPlayer()
    {
        int lastIndex = 0;
        if(IsClockWise)
        {
            lastIndex =CurPlayerIndex - 1;
            lastIndex = lastIndex < 0 ? _players.Count - 1 : lastIndex;
        }
        else
        {
            lastIndex =CurPlayerIndex + 1;
            lastIndex = lastIndex > _players.Count - 1 ? 0 : lastIndex;
        }

        return _players[lastIndex];
    }

    public Player GetNextPlayer()
    {
        int nextPlayer = 0;

        if (IsClockWise)
        {
            nextPlayer = CurPlayerIndex + 1;
            nextPlayer = nextPlayer > _players.Count - 1 ? 0 : nextPlayer;
        }
        else
        {
            nextPlayer = CurPlayerIndex - 1;
            nextPlayer = nextPlayer < 0 ? _players.Count - 1 : nextPlayer;
        }

        return _players[nextPlayer];
    }

    /// <summary>
    /// 更新轮次，轮到下一家出牌
    /// </summary>
    public void UpdateCurPlayerIndex()
    {
        if (IsClockWise)
        {
            CurPlayerIndex++;
            CurPlayerIndex = CurPlayerIndex > _players.Count - 1 ? 0 : CurPlayerIndex;
        }
        else
        {
            CurPlayerIndex--;
            CurPlayerIndex = CurPlayerIndex < 0 ? _players.Count - 1 : CurPlayerIndex;
        }
    }

    /// <summary>
    /// 是否轮到你出牌
    /// </summary>
    /// <returns></returns>
    public bool IsCanPlayByTurn()
    {
        if(CurPlayerIndex == Utils.ClientLocalPlayer().UID)
        {
            return true;
        }
        Debug.Log("is not your turn");
        return false;
    }
}
