using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;

public class LobbyPlayerListCustom : Prototype.NetworkLobby.LobbyPlayerList
{
	public static LobbyPlayerListCustom GetInstance(){
		return (LobbyPlayerListCustom)_instance;
	}


    public List<LobbyPlayer>  GetPlayerList()
    {
        return _players;
    }

}
