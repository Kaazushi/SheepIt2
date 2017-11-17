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
	Dictionary<int, int> m_points = new Dictionary<int, int>();
    Dictionary<int, GameObject> m_dictionnaryPlayers = new Dictionary<int, GameObject>();
	public List<PlayerInfo> m_playerList = new List<PlayerInfo>();

    NetworkStartPosition[] spawnPoints;

    int m_preda = -1;
    int currentSpawn = 0;

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
        StartCoroutine(BeginGameCoroutine());
    }

    IEnumerator BeginGameCoroutine()
    {
        yield return new WaitForSeconds(2);

        m_players = GameObject.FindGameObjectsWithTag("Player");
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        currentSpawn = 0;
        foreach (GameObject go in m_players)
        {
            if (isServer)
            {
                Debug.Log(go.name + "  " + go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId);

            }
            Debug.Log(go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId);
            m_dictionnaryPlayers[go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId] = go;
            m_points[go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId] = 0;
            Debug.Log(m_playerList.Count);
            go.GetComponent<PlayerController>().RpcDisplayMyColor(m_playerList.Find(o => o._playerID == go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId)._playercolor);
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
            BeginGame();
            return;
        }
        for(int i = 0; i < m_players.Length; i++)
        {
            AnimalType type;
            if(i == m_preda)
            {
                type = AnimalType.WOLF;
                m_players[i].GetComponent<PlayerController>().RpcSetPredator(true);
            }
            else
            {
                type = AnimalType.SHEEP;
                m_players[i].GetComponent<PlayerController>().RpcSetPredator(false);

            }

            m_players[i].GetComponent<PlayerController>().RpcSetSkin(type);
            m_players[i].GetComponent<PlayerController>().RpcSetPosition(spawnPoints[currentSpawn%spawnPoints.Length].transform.position);
            currentSpawn++;

        }
    }

    [Command]
    public void CmdAddPoint(int a_predator, int a_victim)
    {
        Debug.Log("POIIIINT pour " + a_predator);
        m_dictionnaryPlayers[a_victim].GetComponent<PlayerController>().RpcDestroyYourSkin();
        m_points[a_predator]++;
        if (m_points[a_predator] == m_players.Length -1)
        {
            StartRound();
        }
    }

}