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

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.CompareTag("PlayerSkin"))
		{
			bool isCollPredator = coll.gameObject.GetComponent<IPlayerController> ().getIsPredator ();
			//if this object is a predator and the collison is a prey
			if (isPredator && !isCollPredator) {
				Debug.Log ("Collided between predator and prey");
				//desactiver le skin de la proie (à améliorer probablement)
				coll.gameObject.transform.GetChild(0).gameObject.Destroy();

				GameManager.INSTANCE.CmdAddPoint(gameObject.GetComponent<NetworkIdentity>().netId);
			}
		}
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


	public bool getIsPredator(){
		return isPredator;
	}
}
