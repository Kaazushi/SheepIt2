using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer {

	float m_startTime;
	public float m_roundTime;
	bool m_running;
	public float m_currentTime;

	public Timer(float i_roundTime){
		m_startTime = 0;
		m_roundTime = i_roundTime;
		m_running = false;
	}

	public void TimerStartRound(){
		m_startTime = Time.time;
		m_running = true;

		m_currentTime = 0;
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
		m_currentTime += Time.deltaTime;
	}

	public bool IsTimerRunning(){
		return m_running;
	}
}
