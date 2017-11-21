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
	public List<PlayerInfo> m_playerList = new List<PlayerInfo>();

    NetworkStartPosition[] spawnPoints;

    int m_preda = -1;
    int currentSpawn = 0;

    void Start()
    {
        if (INSTANCE != null && INSTANCE != this && isServer)
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
        Debug.Log("BEGIN GAME");

        m_players = GameObject.FindGameObjectsWithTag("Player");
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        currentSpawn = 0;
        foreach (GameObject go in m_players)
        {
            if (isServer)
            {
                Debug.Log(go.name + "  " + go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId);
            }
            PlayerInfo playerInfo = GetPlayerInfo(go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId);
            playerInfo._playerController = go.GetComponent<PlayerController> ();
            playerInfo._playerScore = 0;

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
        Debug.Log("POINT pour " + a_predator);
		GetPlayerInfo(a_victim)._playerController.RpcDestroyYourSkin();
		PlayerInfo predaInfos = GetPlayerInfo (a_predator);
		predaInfos._playerScore++;

        Debug.Log("PILOU : " + predaInfos._playerScore + "    " + (m_players.Length - 1));

        //Temp round stoping criteria
        if (predaInfos._playerScore == m_players.Length -1)
        {
            StartRound();
        }
    }

	//TODO: Throws Exception (when player not found)
	public PlayerInfo GetPlayerInfo(int iConnectionID){
		PlayerInfo target;
		target = m_playerList.Find (o=>o._playerID == iConnectionID);

		// Envoyer exception si target == null
		if (target == null) {
			Debug.Log ("Player " + iConnectionID + " not found!");
		}
		return target;
	}


	[Command]
	public List<PlayerInfo> GetPlayerInfoList(){
		return m_playerList;
	}
}