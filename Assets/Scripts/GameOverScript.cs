using UnityEngine;
using System.Collections;

public class GameOverScript : Photon.MonoBehaviour {

	private GUISkin skin;

	void Awake()
	{
		skin = Resources.Load ("GUISkin") as GUISkin;
	}
	void OnGUI()
	{
		GUI.skin = skin;

		GUI.Label(new Rect(3 * Screen.width/5 - 50, 2 * Screen.height/5, 400, 100), "Score: " + PhotonNetwork.player.GetScore());

		if(GUI.Button (new Rect(3 * Screen.width/5, 3 * Screen.height/5, 250, 125), "Main\nMenu"))
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
