using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityStrategy : MonoBehaviour {

	// Movement
	public virtual void PlayerMovement(GameObject iPlayer){}

	// Ability1
	public virtual void Ability1(){}

	// Ability2
	public virtual void Ability2(){}

	// Death
	public virtual void PlayerDeath(){}
}
