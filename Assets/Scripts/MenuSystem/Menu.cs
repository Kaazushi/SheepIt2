using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
	public void CloseMenu()
	{
		MenuManager.INSTANCE.CloseMenu();
	}


    public virtual float GetAlphaBack()
    {
        return 1.0f;
    }
}