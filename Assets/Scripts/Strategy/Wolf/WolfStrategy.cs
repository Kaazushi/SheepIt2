using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WolfStrategy : AbilityStrategy {

    [SerializeField]
    [SerializeField]
    float m_timeFear = 5.0f;
    [SerializeField]
    float m_speedFear = 4.0f;


    // Movement
    public override void PlayerMovement(){
		base.PlayerMovement ();
	}

	// Ability1
	public override void Ability1() {

        Debug.Log("World use fear");
        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        foreach(PlayerInfo info in list)
        {
            if (info.gameObject != m_player && (info.gameObject.transform.position - transform.position).magnitude <= m_distanceFear)
            {
            }
        }

    }


    // Ability2
    public override void Ability2() { }

	// Death
	public override void PlayerDeath(){}
}
