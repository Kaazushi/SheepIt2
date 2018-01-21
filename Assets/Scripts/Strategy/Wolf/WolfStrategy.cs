using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WolfStrategy : AbilityStrategy {

	// Movement
	public override void PlayerMovement(){
		base.PlayerMovement ();
	}

	// Ability1
	public override void Ability1() {

        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        foreach(PlayerInfo info in list)
        {
            if (info.gameObject != m_player)
            {
                info.gameObject.GetComponent<PlayerController>().RpcForcePath(m_player.transform.position, 1, 5);
            }
        }

    }


    // Ability2
    public override void Ability2() { }

	// Death
	public override void PlayerDeath(){}
}
