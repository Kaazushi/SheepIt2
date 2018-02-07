using Prototype.NetworkLobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public enum AnimalType { SHEEP, WOLF}

public class GameManager : NetworkBehaviour
{

    public static GameManager INSTANCE;

    [SerializeField]
    GameObject m_spawnObjectsContainer;

	HUDManager m_hud;

	Timer m_timerRound;
	float m_roundMaxTime = 1000;

    NetworkStartPosition[] m_spawnPoints;

    bool m_isInit = false;

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

        m_timerRound = TimerFactory.INSTANCE.getTimer();
        
    }

	void Update(){
		if (m_timerRound.IsTimerRunning()) {
			//Display time in UI
			m_hud.RpcSetTimerTime (m_timerRound + "");
		}
	}


    public void Init()
    {
        if (m_isInit)
        {
            return;
        }
        m_hud = GameObject.FindGameObjectWithTag("UI").GetComponent<HUDManager>();

        StartCoroutine(InitCoroutine());


        m_isInit = true;
    }


    IEnumerator InitCoroutine()
    {
        yield return new WaitForSeconds(2);
        List<LobbyPlayer> _lobbyPlayerList = LobbyPlayerListCustom.GetInstance().GetPlayerList();

        GameData.INSTANCE.RpcRetrievePlayerInfo();
        //Rpc notsynchronize so get on server separatly
        GameData.INSTANCE.RetrievePlayerInfo();

        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        m_spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        foreach (PlayerInfo playerInfo in list)
        {
            LobbyPlayer lobbyPlayer = _lobbyPlayerList.Find(o => o.connectionToClient.connectionId == playerInfo.GetPlayerId());
            playerInfo.setData(lobbyPlayer.playerColor, lobbyPlayer.playerName);
        }



        BeginGame();
    }


    void BeginGame()
    {
        Debug.Log("BEGIN GAME");

        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        foreach (PlayerInfo info in list)
        {
            info.Score = 0;
        }

        currentSpawn = 0;
        m_preda = -1;

        StartRound();

    }



    private void StartRound()
    {
        Util.DestroyChilds(m_spawnObjectsContainer.transform);
  

        ++m_preda;
        if (m_preda >= GameData.INSTANCE.GetNumberPlayer())
        {
            //tout le monde a été prédateur
            BeginGame();
            return;
        }

        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();

        int i = 0;
        foreach (PlayerInfo playerInfo in list)
        {
            AnimalType type;
            if(i == m_preda)
            {
                playerInfo.IsPreda = true;
                type = AnimalType.WOLF;
            }
            else
            {
                playerInfo.IsPreda = false;
                type = AnimalType.SHEEP;
            }
            playerInfo.gameObject.GetComponent<PlayerController>().RpcSetSkin(type);
            playerInfo.gameObject.GetComponent<PlayerController>().RpcSetPosition(m_spawnPoints[currentSpawn%m_spawnPoints.Length].transform.position);
            playerInfo.IsAlive = true;
            ++currentSpawn;
            ++i;
        }

		m_timerRound.StartTimer (m_roundMaxTime, () => { EndOfRound(); /*StartRound();*/ });
    }


    private void EndOfRound()
    {
        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        foreach (PlayerInfo playerInfo in list)
        {
            if(playerInfo.IsAlive && !playerInfo.IsPreda)
            {
                playerInfo.IncrementScore();
            }

        }


            StartRound();
    }

    //think to change the way to put a preda and handle the TAB disparition && synchronize the gamedata remove && anchor of tab menu && finir le jeu si ya plus personne en vie après la deco
    // doit^^etreconnectionToServer du côté client et connectionToClient du côté client
    [Command]
    public void CmdAddPoint(int a_predator, int a_victim)
    {
        PlayerController victim = GameData.INSTANCE.GetPlayerInfo(a_victim).gameObject.GetComponent<PlayerController>();
        victim.RpcDestroyYourSkin();
        victim.RpcDestroyYourAbility();
        GameData.INSTANCE.GetPlayerInfo(a_victim).IsAlive = false;
        PlayerInfo predaInfos = GameData.INSTANCE.GetPlayerInfo (a_predator);
		predaInfos.IncrementScore();


        List<PlayerInfo> alive = GameData.INSTANCE.GetPlayerInfoList().FindAll(o => o.IsAlive && !o.IsPreda);
        if (alive.Count == 0)
        {
            EndOfRound();
        }
    }

    public void SpawnObject(GameObject a_object, Vector3 a_position, Quaternion a_rotation)
    {
        if (isServer)
        {
            NetworkServer.Spawn(Instantiate(a_object, a_position, a_rotation, m_spawnObjectsContainer.transform));
        }
    }
}