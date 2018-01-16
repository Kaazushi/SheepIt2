using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WolfStrategy : AbilityStrategy {

	// Movement
	public override void PlayerMovement(GameObject iPlayer){
		base.PlayerMovement (iPlayer);
	}

	// Ability1
	public override void Ability1(GameObject iPlayer) {

        List<PlayerInfo> list = GameData.INSTANCE.GetPlayerInfoList();
        foreach(PlayerInfo info in list)
        {
            if (info.gameObject != iPlayer)
            {
                info.gameObject.GetComponent<PlayerController>().RpcForcePath(iPlayer.transform.position, 1, 5);
            }
        }

    }


    // Ability2
    public override void Ability2() { }

	// Death
	public override void PlayerDeath(){}
}
