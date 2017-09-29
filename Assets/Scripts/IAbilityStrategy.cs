using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAbilityStrategy : MonoBehaviour {

	// Movement
	private void PlayerMovement(GameObject iPlayer);

	// Ability1
	private void Ability1();

	// Ability2
	private void Ability2();

	// Death
	private void PlayerDeath();
}
