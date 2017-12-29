using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WolfStrategy : AbilityStrategy {

	// Movement
	public override void PlayerMovement(GameObject iPlayer){
		base.PlayerMovement (iPlayer);
	}


    // Ability1
    public override void Ability1(){

    }

    // Ability2
    public override void Ability2() { }

	// Death
	public override void PlayerDeath(){}
}
