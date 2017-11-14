using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo {

	public int _playerID;
	public Color _playercolor;
	public string _playerName;

	public PlayerInfo(int iPlayerID, Color iPlayerColor, string iPlayerName){
		_playerID = iPlayerID;
		_playercolor = iPlayerColor;
		_playerName = iPlayerName;
	}
}
