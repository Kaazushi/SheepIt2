using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManagerCustom : Prototype.NetworkLobby.LobbyManager
{
    List<NetworkConnection> m_clients = new List<NetworkConnection>();

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
        base.OnServerSceneChanged(sceneName);
        GameManager.INSTANCE.Init();
    }

}
