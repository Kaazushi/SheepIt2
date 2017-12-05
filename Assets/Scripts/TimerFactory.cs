using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFactory : MonoBehaviour {

	public static TimerFactory INSTANCE;

	void Start(){
		if (INSTANCE != null && INSTANCE != this)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			INSTANCE = this;
			DontDestroyOnLoad(this);
		}
	}

	public Timer getTimer(){
		return gameObject.AddComponent<Timer>();
	}
}