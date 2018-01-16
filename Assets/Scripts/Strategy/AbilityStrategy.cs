using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbilityStrategy : MonoBehaviour {

	[SerializeField]
	private float _Speed = 5;
    int m_fixedSpeed = 0;
    Vector3 m_fixedDirection;

    // Movement
    public virtual void PlayerMovement(GameObject iPlayer)
	{
		// Handle simple movement
		Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 playerPos = iPlayer.transform.position;

		if(Vector2.Distance(mousePos, playerPos) > 0.05f)
		{

			Vector2 direction = m_fixedSpeed != 0 ? new Vector2(m_fixedDirection.x, m_fixedDirection.y) : mousePos - playerPos;
			direction.Normalize();

            Debug.Log(direction);

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
			iPlayer.GetComponent<Rigidbody2D>().angularVelocity = 0;
			//iPlayer.GetComponent<Rigidbody2D>().angularDrag = 1;
        }
	}

	// Ability1
	public virtual void Ability1(GameObject iPlayer) {}

    public void ForcePath(Vector3 a_position, int a_speed, int a_time)
    {
        m_fixedDirection = -1 *(a_position - gameObject.transform.position);
        m_fixedSpeed = a_speed;
    }


    // Ability2
    public virtual void Ability2(){}

	// Death
	public virtual void PlayerDeath(){}
}
