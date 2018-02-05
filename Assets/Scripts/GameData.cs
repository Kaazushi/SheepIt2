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

    [ClientRpc]
    public void RpcRetrievePlayerInfo()
    {
        if (!isServer)
        {
            RetrievePlayerInfo();
        }
    }

    public void RetrievePlayerInfo()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in players)
        {
            AddPlayerInfo(go.GetComponent<PlayerInfo>());
        }
    }



    void AddPlayerInfo(PlayerInfo a_playerInfo)
    {
        if (!m_playerList.Contains(a_playerInfo))
        {
            m_playerList.Add(a_playerInfo);
        }
    }

    [ClientRpc]
    public void RpcDeletePlayerInfoObsolete(int id)
    {
        Debug.Log("Delete Obsolete");
        for(int i = m_playerList.Count - 1; i >= 0; --i)
        {
            if(m_playerList[i].connectionToClient == null || m_playerList[i].GetPlayerId() == id)
            {

                m_playerList.RemoveAt(i);

            }
        }
    }

    public PlayerInfo GetPlayerInfo(int iConnectionID)
    {
        PlayerInfo target;
        target = m_playerList.Find(o => o.GetPlayerId() == iConnectionID);

        // Envoyer exception si target == null
        if (target == null)
        {
            Debug.Log("Player " + iConnectionID + " not found!");
            throw new System.MissingMemberException();

        }
        return target;
    }

    public List<PlayerInfo> GetPlayerInfoList()
    {
        return m_playerList;
    }

    public int GetNumberPlayer()
    {
        return m_playerList.Count;
    }

    private void Update()
    {
    }

}
