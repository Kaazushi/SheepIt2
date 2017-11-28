using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

	GameObject m_timerUI;

	float m_startTime;
	float m_roundTime;
	bool m_running;
	int m_currentTime;

	public Timer(float i_roundTime){
		m_startTime = 0;
		m_roundTime = i_roundTime;
		m_running = false;
	}

	public void TimerStartRound(){
		m_startTime = Time.time;
		m_running = true;

		m_timerUI = GameObject.FindGameObjectWithTag ("UI");
		if (m_timerUI == null)
			Debug.Log ("UI not found");
	}

	public bool IsTimeUp(){
		if (m_running) {
			if (Time.time >= (m_startTime + m_roundTime)) {
				m_running = false;
				return true;
			} else
				return false;
		} 
		else
			return false;
	}

	void Update () {
		if (!m_running) {
			return;
		}
		m_currentTime = (int)(Time.time - m_startTime);

		//Display time in UI
		float timeLeft = m_roundTime - m_currentTime;
		string minLeft = ((int)timeLeft / 60).ToString();
		string secLeft = ((int)timeLeft % 60).ToString();

		m_timerUI.GetComponent<HUDManager> ().RpcSetTimerTime (minLeft + ":" + secLeft);
	}
}
