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

	public HUDManager m_hud;

	Timer m_timer;
	float m_roundMaxTime = 20;

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
		m_timer = new Timer (m_roundMaxTime);
    }

	void Update(){
		if (m_timer.IsTimerRunning()) {
			//Display time in UI
			float timeLeft = m_timer.m_roundTime - m_timer.m_currentTime;
			string minLeft = ((int)timeLeft / 60).ToString ();
			string secLeft = ((int)timeLeft % 60).ToString ();

			m_hud.RpcSetTimerTime (minLeft + ":" + secLeft);
		}
	}

    void LateUpdate()
    {
		if (m_timer.IsTimeUp ()) {
			StartRound();
		}
    }

    public void BeginGame()
    {
		m_hud = GameObject.FindGameObjectWithTag ("UI").GetComponent<HUDManager> ();
		if (m_hud != null) {
			Debug.Log (m_hud);
		}
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
            PlayerInfo playerInfo = GameData.INSTANCE.GetPlayerInfo(go.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId);
            Debug.Log(playerInfo);
            playerInfo._playerController = go.GetComponent<PlayerController> ();
            playerInfo._playerScore = 0;

            go.GetComponent<PlayerController>().RpcDisplayMyColor(playerInfo._playercolor);
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

		m_timer.TimerStartRound ();
    }


    [Command]
    public void CmdAddPoint(int a_predator, int a_victim)
    {
        Debug.Log("POINT pour " + a_predator);
		GameData.INSTANCE.GetPlayerInfo(a_victim)._playerController.RpcDestroyYourSkin();
		PlayerInfo predaInfos = GameData.INSTANCE.GetPlayerInfo (a_predator);
		predaInfos._playerScore++;


        //Temp round stoping criteria
        if (predaInfos._playerScore == m_players.Length -1)
        {
            StartRound();
        }
    }

}