using UnityEngine;
using System.Collections;

public class PickPlayerScript : Photon.MonoBehaviour {
	
	private GUISkin skin;

	void Awake()
	{
		skin = Resources.Load ("GUISkin3") as GUISkin;
	}
	void OnGUI()
	{
		skin = Resources.Load ("GUISkinCY1") as GUISkin;
		GUI.skin = skin;
		if(GUI.Button (new Rect(2 * Screen.width/6 - 250, 2 * Screen.height/5, 250, 250), ""))
		{
			PhotonNetwork.playerName = "Player1";
			PhotonNetwork.JoinRoom("myRoom");
		}
		skin = Resources.Load ("GUISkinCY2") as GUISkin;
		GUI.skin = skin;
		if(GUI.Button (new Rect(3 * Screen.width/6 - 250, 2 * Screen.height/5, 250, 250), ""))
		{
			PhotonNetwork.playerName = "Player2";
			PhotonNetwork.JoinRoom("myRoom");
		}
		skin = Resources.Load ("GUISkinCY3") as GUISkin;
		GUI.skin = skin;
		if(GUI.Button (new Rect(4 * Screen.width/6 - 250, 2 * Screen.height/5, 250, 250), ""))
		{
			PhotonNetwork.playerName = "Player3";
			PhotonNetwork.JoinRoom("myRoom");
		}
		skin = Resources.Load ("GUISkinCY4") as GUISkin;
		GUI.skin = skin;
		if(GUI.Button (new Rect(5 * Screen.width/6 - 250, 2 * Screen.height/5, 250, 250), ""))
		{
			PhotonNetwork.playerName = "Player4";
			PhotonNetwork.JoinRoom("myRoom");
		}
		skin = Resources.Load ("GUISkin") as GUISkin;
		GUI.skin = skin;
		if(GUI.Button (new Rect(Screen.width/2 - 125, 4 * Screen.height/5, 250, 125), "Main\nMenu"))
		{
			Application.LoadLevel("Menu");
		}
	}
}


