using UnityEngine;
using System.Collections;

public class AboutScript : Photon.MonoBehaviour {
	
	private GUISkin skin;
	
	void Awake()
	{
		skin = Resources.Load ("GUISkin") as GUISkin;
	}
	void OnGUI()
	{
		GUI.skin = skin;

		if(GUI.Button (new Rect(Screen.width/2 - 125, 4 * Screen.height/5, 250, 125), "Main\nMenu"))
		{
			Application.LoadLevel("Menu");
		}
	}
}

