﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepStrategy : AbilityStrategy {

	// Movement
	public override void PlayerMovement(GameObject iPlayer)
	{
		base.PlayerMovement (iPlayer);
	}


	// Ability1: spawn a Fence
	private float ab1_cd = 0;
	private float ab1_last_call = 0;

	public override void Ability1(GameObject iPlayer)
    {
		float current_time = Time.time;
        // if Ability is available : spawn the prefab
		if (current_time > ab1_last_call + ab1_cd) {
			Debug.Log ("Using Fence");
            GameObject fence = (GameObject) Resources.Load("Fence", typeof(GameObject));
            Vector3 position = iPlayer.transform.position;
            Matrix4x4 m = Matrix4x4.Translate(-iPlayer.transform.right);
            position = m.MultiplyPoint3x4(position);

            Quaternion rotation = iPlayer.transform.rotation;
            

            GameManager.INSTANCE.CmdSpawnObject(fence, position, rotation);

			ab1_last_call = current_time;
		} 
        // if Ability on cooldown : do nothing
		else {
			Debug.Log ("Fence is on cooldown");
		}
	}

	// Ability2
	public override void Ability2(){}

	// Death
	public override void PlayerDeath(){}

}
