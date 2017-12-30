using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheepFence : NetworkBehaviour {
    Timer m_timer;


    void Start () {
        if (isServer)
        {
            m_timer = TimerFactory.INSTANCE.getTimer();
            m_timer.StartTimer(4, () => { Destroy(gameObject); });
        }
    }
	
	// Update is called once per frame
	void Update () {
	}
}
