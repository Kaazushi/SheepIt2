using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Menu : MonoBehaviour
{
	public void CloseMenu()
	{
		MenuManager.CloseMenu();
	}

	/// <summary>
	/// If true the background of menu is displayed
	/// </summary>
	public virtual bool DisplayBack()
	{
		return true;
	}

}