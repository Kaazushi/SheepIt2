using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IPlayerController : NetworkBehaviour {


	private AbilityStrategy _Strat = new SheepStrategy();

	[ClientRpc]
	public void RpcSetSkin(Skin skin, AbilityStrategy strat)
	{
		//change player skin
		gameObject.transform.SetParent(skin.gameObject.transform);
		//set corresponding strategy
		_Strat = strat;
	}


	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}
	
		_Strat.PlayerMovement(gameObject);

	}
}
