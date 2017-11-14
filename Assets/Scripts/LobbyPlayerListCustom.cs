using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class LobbyPlayerListCustom : Prototype.NetworkLobby.LobbyPlayerList
{

    public List<LobbyPlayer>  GetPlayerList()
    {
        return _players;
    }

}
