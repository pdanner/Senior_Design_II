using UnityEngine;
using System.Collections;

public class GameWinScript : Photon.MonoBehaviour {
	
	private GUISkin skin;
	
	void Awake()
	{
		skin = Resources.Load ("GUISkin3") as GUISkin;
	}
	void OnGUI()
	{
		GUI.skin = skin;
		if(!PhotonNetwork.playerName.ToLower().Equals("windows"))
			GUI.Label(new Rect(1 * Screen.width/5 - 50, 1 * Screen.height/5 + 30, 400, 100), "Score: " + PhotonNetwork.player.GetScore());
		
		if(GUI.Button (new Rect(1 * Screen.width/5, 2 * Screen.height/5, 250, 125), "Main\nMenu"))
		{
			if(PhotonNetwork.connected)
			{
				PhotonNetwork.LeaveRoom();
				Application.LoadLevel ("Menu");
				Destroy(this.gameObject);
			}
			else
			{
				Application.LoadLevel ("Menu");
				Destroy(this.gameObject);
			}
		}
	}
}

