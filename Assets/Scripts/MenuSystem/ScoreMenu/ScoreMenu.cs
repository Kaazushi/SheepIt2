using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMenu : Menu {
    private void OnEnable()
    {
        //start coroutine wich get score from gamemanager
       //  Debug.Log(GameManager.INSTANCE.m_playerList);
    }

    private void OnDisable()
    {
        //stop coroutine
    }

    public override float GetAlphaBack()
    {
        return 0.8f;
    }


}
