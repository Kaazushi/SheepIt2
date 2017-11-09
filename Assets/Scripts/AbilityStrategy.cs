using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityStrategy {

	[SerializeField]
	private float _Speed = 5;

	// Movement
	public virtual void PlayerMovement(GameObject iPlayer)
	{
		// Handle simple movement
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 playerPos = iPlayer.transform.position;

		if(Vector2.Distance(mousePos, playerPos) > 0.05f)
		{
			Vector2 direction = mousePos - playerPos;
			direction.Normalize();

			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
			{
				iPlayer.GetComponent<Rigidbody2D>().velocity = direction * _Speed;
			}

			Vector3 movingDir = new Vector3(direction.x, direction.y, 0f);
			float angle = Mathf.Atan2(movingDir.y, movingDir.x) * Mathf.Rad2Deg;
			iPlayer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		else
		{
			iPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		if (!Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.UpArrow))
		{
			iPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}

	// Ability1
	public virtual void Ability1(){}

	// Ability2
	public virtual void Ability2(){}

	// Death
	public virtual void PlayerDeath(){}
}
