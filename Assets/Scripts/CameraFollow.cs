using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject m_player;

	// Use this for initialization
	void Start () {
        m_player = null;
    }
	
	// Update is called once per frame
	void Update () {
        if(m_player != null)
        {
            gameObject.transform.position = m_player.transform.position;
        }
	}

    public void SetPlayerToFollow(GameObject i_player)
    {
        m_player = i_player;
    }
}
