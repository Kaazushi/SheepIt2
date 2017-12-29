using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoDisplayer : MonoBehaviour {

    [SerializeField]
    Text m_name;

    [SerializeField]
    Text m_score;

    PlayerInfo m_playerInfo;
    // Update is called once per frame
    void Update () {
        //Debug.Log(m_playerInfo);
        if (m_playerInfo)
        {
            m_name.text = m_playerInfo._playerName;
            m_score.text = m_playerInfo._playerScore + "";
        }
	}

    public void SetPlayerInfo(PlayerInfo a_playerInfo)
    {
        Debug.Log("pilou");
        m_playerInfo = a_playerInfo;
    }

}
