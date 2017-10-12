using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum AnimalType { SHEEP, WOLF}

public class GameManager : MonoBehaviour
{

    public static GameManager INSTANCE;
    GameObject[] m_players;
    Dictionary<GameObject, int> m_points = new Dictionary<GameObject, int>();
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
            m_points[go] = 0;
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
            Skin skin;
            AbilityStrategy strat;
            if(i == m_preda)
            {
                skin = SkinFactory.INSTANCE.getSkin(AnimalType.WOLF);
                strat = AbilityStrategyFactory.INSTANCE.getAbilityStrategy(AnimalType.WOLF);
                //m_players[m_preda].GetComponent<IPlayerController>().SetPreda(true);
            }
            else
            {
                skin = SkinFactory.INSTANCE.getSkin(AnimalType.SHEEP);
                strat = AbilityStrategyFactory.INSTANCE.getAbilityStrategy(AnimalType.SHEEP);
            }
            m_players[i].GetComponent<IPlayerController>().RpcSetSkin(skin,strat);
        }
    }

    [Command]
    void CmdAddPoint(GameObject a_player)
    {
        m_points[a_player]++;
        if (m_points[a_player] == m_players.Length -1)
        {
            StartRound();
        }
    }

}