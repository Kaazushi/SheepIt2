using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType { SHEEP, WOLF}

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;
    void Start () {
		if(INSTANCE != null && INSTANCE != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            INSTANCE = this;
            DontDestroyOnLoad(this);
        }
	}
	

	void Update () {
	}
}
