using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IPlayerController : NetworkBehaviour {


	private AbilityStrategy _Strat = new SheepStrategy();


	public void SetSkin(Skin skin, AbilityStrategy strat)
	{
		//change player skin

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
