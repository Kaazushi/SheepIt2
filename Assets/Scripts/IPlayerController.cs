using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IPlayerController : NetworkBehaviour {

	private bool isPredator = false;
	private AbilityStrategy _Strat = new SheepStrategy();

	[ClientRpc]
	public void RpcSetSkin(AnimalType type)
	{
		gameObject.SetActive (true);

        GameObject skin = SkinFactory.INSTANCE.getSkin (type);
		skin.transform.SetParent(gameObject.transform, false);

		//set corresponding strategy
		_Strat = AbilityStrategyFactory.INSTANCE.getAbilityStrategy(type);

	}


	[ClientRpc]
	public void RpcSetPredator (bool isPred){
		isPredator = isPred;
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
