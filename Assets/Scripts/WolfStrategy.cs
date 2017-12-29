using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStrategy : AbilityStrategy {

	// Movement
	public override void PlayerMovement(GameObject iPlayer){
		base.PlayerMovement (iPlayer);
	}

	// Ability1
	public override void Ability1(GameObject iPlayer) {}

	// Ability2
	public override void Ability2(){}

	// Death
	public override void PlayerDeath(){}
}
