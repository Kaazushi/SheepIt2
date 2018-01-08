﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {


	private bool isPredator = false;
	private AbilityStrategy _Strat = new SheepStrategy();

	[ClientRpc]
	public void RpcSetSkin(AnimalType type)
	{
			if (gameObject.GetComponentInChildren<Skin> () != null) {
				Destroy (gameObject.GetComponentInChildren<Skin> ().gameObject);
			}

            GameObject skin = SkinFactory.INSTANCE.getSkin(type);
            skin.transform.SetParent(gameObject.transform, false);

            //set corresponding strategy
            
            GameObject strategy = AbilityStrategyFactory.INSTANCE.getAbilityStrategy(type);
            strategy.transform.SetParent(gameObject.transform, false);
            _Strat = strategy.GetComponent<AbilityStrategy>();

    }

    void OnCollisionEnter2D(Collision2D coll)
	{
        if (isServer)
        {
            if (coll.collider.CompareTag("PlayerSkin"))
            {
                bool isCollPredator = coll.gameObject.GetComponent<PlayerController>().getIsPredator();
                //if this object is a predator and the collison is a prey
                if (!isPredator && isCollPredator)
                {
                    DestroyYourSkin();
                    DestroyYourAbility();

                    Debug.Log("Collided between predator and prey");
                    GameManager.INSTANCE.CmdAddPoint(coll.gameObject.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId, gameObject.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId);
                }
            }
        }
    
    }


    [ClientRpc]
    public void RpcDestroyYourAbility()
    {
        DestroyYourAbility();
    }

    void DestroyYourAbility()
    {
        if (_Strat)
        {
            Destroy(_Strat.gameObject);
        }
    }



    [ClientRpc]
    public void RpcDestroyYourSkin()
    {
        DestroyYourSkin();
    }

    void  DestroyYourSkin()
    {
        //desactiver le skin de la proie (à améliorer probablement)
        Skin skin = gameObject.GetComponentInChildren<Skin>();
        if (skin)
        {
            Destroy(skin.gameObject);
        }
    }


    [ClientRpc]
	public void RpcSetPredator (bool isPred){
		isPredator = isPred;
	}

    [ClientRpc]
    public void RpcDisplayMyColor(Color color)
    {
        if (isLocalPlayer)
        {
            Camera.main.backgroundColor = color;
        }
    }

    void DisplayMyColor(Color color)
    {
        if (isLocalPlayer)
        {
            Camera.main.backgroundColor = color;
        }
    }

    [ClientRpc]
    public void RpcSetPosition(Vector3 a_position)
    {
        if (isLocalPlayer)
        {
            transform.position = a_position;
        }
    }


    [ClientRpc]
    public void RpcForcePath(Vector3 a_position,int a_speed, int a_time)
    {
        if (isLocalPlayer)
        {

        }
    }


    // Update is called once per frame
    void Update () {
		if (!isLocalPlayer)
		{
			return;
		}

		//ScoreMenu
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuManager.INSTANCE.OpenMenu(MENUTYPE.SCORE);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            MenuManager.INSTANCE.CloseMenu();
        }

		//Ability1
		if(Input.GetKeyDown(KeyCode.A)){
			_Strat.Ability1(gameObject);
		}
	
		_Strat.PlayerMovement(gameObject);

    }


    [Command]
    public void CmdSpawnObject(GameObject a_object, Vector3 a_position, Quaternion a_rotation)
    {
        if (isServer)
        {
            Debug.Log("Spawn");
            a_object = (GameObject)Resources.Load("Fence", typeof(GameObject));
            GameManager.INSTANCE.SpawnObject(a_object, a_position, a_rotation);
            //NetworkServer.Spawn(Instantiate(a_object, a_position, a_rotation));
        }
    }


    public bool getIsPredator(){
		return isPredator;
	}
}
