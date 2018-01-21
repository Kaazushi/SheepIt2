using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo  : NetworkBehaviour/*_ pour partager et sync de partout */
{

    [SyncVar(hook = "OnColorChange")]
	Color m_playercolor = Color.black;
    [SyncVar(hook = "OnNameChange")]
    string m_playerName;
    [SyncVar(hook = "OnScoreChange")]
    int m_playerScore;


    public PlayerInfo(Color iPlayerColor, string iPlayerName)
    {
        m_playercolor = iPlayerColor;
        m_playerName = iPlayerName;
        m_playerScore = 0;
    }


    public void SetScore(int a_score)
    {
        m_playerScore = a_score;
    }

    public int GetScore()
    {
        return m_playerScore;
    }


    public string GetName()
    {
        return m_playerName;
    }

    public void IncrementScore()
    {
        ++m_playerScore;
    }



    public void setData(Color playerColor, string playerName)
    {
        m_playercolor = playerColor;
        m_playerName = playerName;
        m_playerScore = 0;
    }

    public int GetPlayerId()
    {
        return connectionToClient.connectionId;
    }


    void OnColorChange(Color a_color)
    {
        Debug.Log("ChangeColor " + a_color);
        m_playercolor = a_color;
    }

    void OnNameChange(String a_name)
    {
        Debug.Log("ChangeName " + a_name);
        m_playerName = a_name;
    }

    void OnScoreChange(int a_score)
    {
        Debug.Log("ChangeScore " + a_score);
        m_playerScore = a_score;
    }




}
