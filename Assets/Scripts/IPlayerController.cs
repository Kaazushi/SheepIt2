using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IPlayerController : NetworkBehaviour {


	private AbilityStrategy _Strat;

	[ClientRpc]
	public void RpcSetSkin(AnimalType type)
	{
		//change player skin
		GameObject skin = SkinFactory.INSTANCE.getSkin (type);

		skin.transform.SetParent(gameObject.transform);

		//set corresponding strategy
		_Strat = AbilityStrategyFactory.INSTANCE.getAbilityStrategy(type);

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
