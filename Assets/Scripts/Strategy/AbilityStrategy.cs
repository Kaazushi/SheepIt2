﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class AbilityStrategy : MonoBehaviour {

	[SerializeField]
	private float _Speed = 5;

    protected GameObject m_player;

    Vector2 m_fearDirection;
    int m_fearSpeed = 0;
    Timer m_fearTimer;

    bool m_isInit = false;

    public void Init( GameObject a_player)
    {
        m_player = a_player;
        m_fearTimer = TimerFactory.INSTANCE.getTimer();

        m_isInit = true;
    }


    //Give direction of movement
    Vector2 GetDirection()
    {
        if (!m_isInit) return Vector2.zero;
        Vector2 result = Vector2.zero;
        if (m_fearTimer && m_fearTimer.IsTimerRunning())
        {
            result =  m_fearDirection;
        }
        else
        {
            // Handle simple movement
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPos = m_player.transform.position;
            if (Vector2.Distance(mousePos, playerPos) > 0.05f )
            {

                result = mousePos - playerPos;
            }

        }
        result.Normalize();

        return result;
    }

    //give speed
    float GetSpeed()
    {
        if (!m_isInit) return 0;

        float result = 0;
        if (m_fearTimer && m_fearTimer.IsTimerRunning())
        {
            result = m_fearSpeed;
        }
        else
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z)))
            {
                result = _Speed;
            }

        }
        return result;
    }



    // Movement
    public virtual void PlayerMovement()
	{
        if (!m_isInit) return;

        Vector2 direction = GetDirection();
        m_player.GetComponent<Rigidbody2D>().velocity = direction * GetSpeed();
		Vector3 movingDir = new Vector3(direction.x, direction.y, 0f);
		float angle = Mathf.Atan2(movingDir.y, movingDir.x) * Mathf.Rad2Deg;
        m_player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

	}

	// Ability1
	public virtual void Ability1() {}

    public void ForcePath(Vector3 a_position, int a_speed, int a_time)
    {
        m_fearDirection = -1 *(a_position - gameObject.transform.position);
        m_fearSpeed = a_speed;
    }


    // Ability2
    public virtual void Ability2(){}

	// Death
	public virtual void PlayerDeath(){}
}
