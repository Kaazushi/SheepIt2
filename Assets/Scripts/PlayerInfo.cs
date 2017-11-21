using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo /*: NetworkBehaviour _ pour partager et sync de partout */
{

	public int _playerID;
	public Color _playercolor;
	public string _playerName;
	public int _playerScore;
	public PlayerController _playerController;

	public PlayerInfo(int iPlayerID, Color iPlayerColor, string iPlayerName){
		_playerID = iPlayerID;
		_playercolor = iPlayerColor;
		_playerName = iPlayerName;
		_playerScore = 0;
		_playerController = null;
	}
}
