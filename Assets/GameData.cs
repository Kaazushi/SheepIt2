using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameData : NetworkBehaviour
{
    List<PlayerInfo> m_playerList = new List<PlayerInfo>();
    static public GameData INSTANCE;
    void Start()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
        }
        else
        {
            INSTANCE = this;
        }
    }

    public void AddPlayerInfo(PlayerInfo a_playerInfo)
    {
        m_playerList.Add(a_playerInfo);
    }

    public PlayerInfo GetPlayerInfo(int iConnectionID)
    {
        PlayerInfo target;
        target = m_playerList.Find(o => o._playerID == iConnectionID);

        // Envoyer exception si target == null
        if (target == null)
        {
            Debug.Log("Player " + iConnectionID + " not found!");
            throw new System.MissingMemberException();

        }
        return target;
    }

}
