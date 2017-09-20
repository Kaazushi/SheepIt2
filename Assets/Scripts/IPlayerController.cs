using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IPlayerController : NetworkBehaviour {

	[SerializeField]
	private float _Speed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}

		// Handle simple movement
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 playerPos = transform.position;

		if(Vector2.Distance(mousePos, playerPos) > 0.05f)
		{
			Vector2 direction = mousePos - playerPos;
			direction.Normalize();

			if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
			{
				GetComponent<Rigidbody2D>().velocity = direction * _Speed;
			}

			Vector3 movingDir = new Vector3(direction.x, direction.y, 0f);
			float angle = Mathf.Atan2(movingDir.y, movingDir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}

		if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.UpArrow))
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
	}
}
