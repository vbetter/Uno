﻿using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();

        NetworkGameMgr.IconIndex++;

        Player player = gamePlayer.GetComponent<Player>();
        player.name = lobby.name;
        player.playerName = lobby.playerName;
        player.color = lobby.playerColor;
        player.IconIndex = NetworkGameMgr.IconIndex;
        player.UID = NetworkGameMgr.IconIndex-1;
    }
}
