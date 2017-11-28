using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HUDManager : NetworkBehaviour {

	[SerializeField]
	Text m_timerDisplay;

	// Set Display Time
	[ClientRpc]
	public void RpcSetTimerTime(string iTextTime){
		m_timerDisplay.text = iTextTime;
	}
}
