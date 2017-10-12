using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IPlayerController : NetworkBehaviour {


	private AbilityStrategy _Strat = new SheepStrategy();

	[ClientRpc]
	public void RpcSetSkin(AnimalType type)
	{
		//change player skin
		Skin skin = SkinFactory.INSTANCE.getSkin (type);
		gameObject.transform.SetParent(skin.gameObject.transform);


		//set corresponding strategy
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
