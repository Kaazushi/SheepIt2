using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WolfStrategy : AbilityStrategy {

    [SerializeField]
    float m_distanceFear = 5.0f;
    [SerializeField]
    float m_timeFear = 5.0f;
    [SerializeField]
    float m_speedFear = 4.0f;


    // Movement
    public override void PlayerMovement(){
		base.PlayerMovement ();
	}

	// Ability1
	protected override void Ability1() {

        Debug.Log("World use fear");
        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        foreach(PlayerInfo info in list)
        {
            if (info.gameObject != m_player && (info.gameObject.transform.position - transform.position).magnitude <= m_distanceFear)
            {
                info.gameObject.GetComponent<PlayerController>().RpcFear(m_player.transform.position, m_speedFear, m_timeFear);
            }
        }

    }


    // Ability2
    protected override void Ability2() { }

	// Death
	public override void PlayerDeath(){}
}
