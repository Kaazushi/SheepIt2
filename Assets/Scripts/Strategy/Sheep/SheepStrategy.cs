using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SheepStrategy : AbilityStrategy {

    [SerializeField]
    GameObject m_fence;

    // Ability1: spawn a Fence
    [SerializeField]
    private float m_spawnFenceCooldown = 5.0f;
    private float ab1_last_call = 0;
    Timer m_spawnFenceTimer;

    private void Start()
    {
        m_spawnFenceTimer = TimerFactory.INSTANCE.getTimer();
        m_spawnFenceTimer.StartTimer(m_spawnFenceCooldown);
        m_spawnFenceTimer.Stop();
    }


    // Movement
    public override void PlayerMovement()
	{
		base.PlayerMovement ();
	}



	public override void Ability1()
    {
		float current_time = Time.time;
        // if Ability is available : spawn the prefab
		if (m_spawnFenceTimer.IsTimeUp()) {
			Debug.Log ("Using Fence");
            Vector3 position = m_player.transform.position;
            Matrix4x4 m = Matrix4x4.Translate(-m_player.transform.right);
            position = m.MultiplyPoint3x4(position);

            Quaternion rotation = m_player.transform.rotation;
            rotation *= Quaternion.Euler(Vector3.forward * 90);

            GameManager.INSTANCE.SpawnObject(m_fence, position, rotation);

            m_spawnFenceTimer.RestartTimer();
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
