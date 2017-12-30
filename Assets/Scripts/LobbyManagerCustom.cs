using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManagerCustom : Prototype.NetworkLobby.LobbyManager
{
    List<NetworkConnection> m_clients = new List<NetworkConnection>();

   // [SerializeField]
   // GameObject m_GameManagerPrefab;

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

    }

    public override void OnServerSceneChanged(string sceneName)
    {
		/*List<LobbyPlayer> _lobbyPlayerList = LobbyPlayerListCustom.GetInstance ().GetPlayerList ();
		foreach(LobbyPlayer pl in _lobbyPlayerList){
			Debug.Log("Player Info : ID " + pl.connectionToClient.connectionId);

            GameData.INSTANCE.AddPlayerInfo (new PlayerInfo(pl.connectionToClient.connectionId, pl.playerColor, pl.playerName));
		}*/
        
        base.OnServerSceneChanged(sceneName);
       // NetworkServer.Spawn(Instantiate(m_GameManagerPrefab));
        GameManager.INSTANCE.BeginGame();

    }

}
