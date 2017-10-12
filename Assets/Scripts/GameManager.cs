using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum AnimalType { SHEEP, WOLF}

public class GameManager : NetworkBehaviour
{

    public static GameManager INSTANCE;
    GameObject[] m_players;
    Dictionary<NetworkInstanceId, int> m_points = new Dictionary<NetworkInstanceId, int>();
    int m_preda = -1;

    void Start()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
    }


    void Update()
    {
    }

    public void BeginGame()
    {
       
        m_players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in m_players)
        {
            
            m_points[go.GetComponent<NetworkIdentity>().netId] = 0;
        }
        m_preda = -1;
        StartRound();
    }

    private void StartRound()
    {
        m_preda++;
        if (m_preda >= m_players.Length)
        {
            //tout le monde a été prédateur
            return;
        }
        for(int i = 0; i < m_players.Length; i++)
        {
            AnimalType type;
            if(i == m_preda)
            {
                type = AnimalType.WOLF;
                //m_players[m_preda].GetComponent<IPlayerController>().SetPreda(true);
            }
            else
            {
                type = AnimalType.SHEEP;
            }
            m_players[i].GetComponent<IPlayerController>().RpcSetSkin(type);
        }
    }

    [Command]
    void CmdAddPoint(NetworkInstanceId a_player)
    {
        m_points[a_player]++;
        if (m_points[a_player] == m_players.Length -1)
        {
            StartRound();
        }
    }

}