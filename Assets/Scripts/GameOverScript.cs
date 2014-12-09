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

		int screenPos = 0;
		for(int i=0; i<PhotonNetwork.playerList.Length; i++)
		{
			if(PhotonNetwork.playerList[i].name.ToLower() != "windows")
			{
				string colorName = "";
				switch(PhotonNetwork.playerList[i].name.ToLower())
				{
				case "player1": colorName = "Red";
					break;
				case "player2": colorName = "Green";
					break;
				case "player3": colorName = "Blue";
					break;
				case "player4": colorName = "Yellow";
					break;
				}
				GUI.Label(new Rect(2 * Screen.width/5 - 50, (screenPos+2) * Screen.height/10, 400, 100), 
				          colorName + ": " + PhotonNetwork.playerList[i].GetScore());
				screenPos++;
			}

		}


		if(GUI.Button (new Rect(4 * Screen.width/5, 2 * Screen.height/4, 250, 125), "Main\nMenu"))
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
