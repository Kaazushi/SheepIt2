using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMenu : Menu {

    [SerializeField]
    GameObject m_PrefabPlayerInfoDisplayer;


    [SerializeField]
    GameObject m_TabDisplay;
    private void OnEnable()
    {
         if(m_TabDisplay.transform.childCount > 0 || !GameData.INSTANCE)
        {
            return;
        }
        //start get score from gamemanager
        foreach (PlayerInfo info in GameData.INSTANCE.GetPlayerInfoList())
        {
            GameObject go = GameObject.Instantiate(m_PrefabPlayerInfoDisplayer);
            go.transform.parent = m_TabDisplay.transform;
            go.GetComponent<PlayerInfoDisplayer>().SetPlayerInfo(info);
        }
    }

    private void OnDisable()
    {
    }

    public override float GetAlphaBack()
    {
        return 0.8f;
    }


}
