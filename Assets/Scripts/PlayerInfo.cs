using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo  : NetworkBehaviour/*_ pour partager et sync de partout */
{

    [SyncVar(hook = "OnColorChange")]
	public Color _playercolor = Color.black;
    [SyncVar(hook = "OnNameChange")]
    public string _playerName;
    [SyncVar(hook = "OnScoreChange")]
    public int _playerScore;

	public PlayerInfo(Color iPlayerColor, string iPlayerName){
		_playercolor = iPlayerColor;
		_playerName = iPlayerName;
		_playerScore = 0;
	}

    public void setData(Color playerColor, string playerName)
    {
        _playercolor = playerColor;
        _playerName = playerName;
        _playerScore = 0;
    }

    public int GetPlayerId()
    {
        return connectionToClient.connectionId;
    }


    void OnColorChange(Color a_color)
    {
        Debug.Log("ChangeColor " + a_color);
        _playercolor = a_color;
    }

    void OnNameChange(String a_name)
    {
        Debug.Log("ChangeName " + a_name);
        _playerName = a_name;
    }

    void OnScoreChange(int a_score)
    {
        Debug.Log("ChangeScore " + a_score);
        _playerScore = a_score;
    }


}
