// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerInGame.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

public class InGameScript : Photon.MonoBehaviour
{
	public Transform playerPrefab1;
	public Transform playerPrefab2;
	public Transform playerPrefab3;
	public Transform playerPrefab4;

	private bool player1Taken = false;
	private bool player2Taken = false;
	private bool player3Taken = false;
	private bool player4Taken = false;

	private bool showStartGame = true;

	private int[] playerPrefabList = new int[4];

	public AudioSource song;

	[RPC] void updatePlayerAvailable(int playerTaken)
	{
		//Debug.Log (playerTaken);
		if(playerTaken == 1) player1Taken = true;
		else if(playerTaken == 2) player2Taken = true;
		else if(playerTaken == 3) player3Taken = true;
		else if(playerTaken == 4) player4Taken = true;
	}

	[RPC] void removePlayer(int playerTaken)
	{
		if(playerTaken == 1) player1Taken = false;
		else if(playerTaken == 2) player2Taken = false;
		else if(playerTaken == 3) player3Taken = false;
		else if(playerTaken == 4) player4Taken = false;
	}

	public void Awake()
	{
		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(TestMenu.SceneNameMenu);
			return;
		}
		// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
		if(PhotonNetwork.playerName.ToLower() != "windows")
		{
			PhotonNetwork.player.SetScore(0);
			if(PhotonNetwork.playerList.Length == 1/*!player1Taken*/)
			{
				PhotonNetwork.Instantiate(this.playerPrefab1.name, transform.position + 
				                         new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f), Quaternion.identity, 0);
				player1Taken = true;
				this.photonView.RPC ("updatePlayerAvailable", PhotonTargets.All, 1);
				playerPrefabList[0] = PhotonNetwork.player.ID;
			}
			else if(PhotonNetwork.playerList.Length == 2/*!player2Taken*/)
			{
				PhotonNetwork.Instantiate(this.playerPrefab2.name, transform.position + 
				                          new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f), Quaternion.identity, 0);
				player2Taken = true;
				this.photonView.RPC ("updatePlayerAvailable", PhotonTargets.All, 2);
				playerPrefabList[1] = PhotonNetwork.player.ID;
			}
			else if(PhotonNetwork.playerList.Length == 3/*!player3Taken*/)
			{
				PhotonNetwork.Instantiate(this.playerPrefab3.name, transform.position + 
				                          new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f), Quaternion.identity, 0);
				player3Taken = true;
				this.photonView.RPC ("updatePlayerAvailable", PhotonTargets.All, 3);
				playerPrefabList[2] = PhotonNetwork.player.ID;
			}
			else if(PhotonNetwork.playerList.Length == 4/*!player4Taken*/)
			{
				PhotonNetwork.Instantiate(this.playerPrefab4.name, transform.position + 
				                          new Vector3(Random.Range(-5,5), Random.Range(-5,5), 0f), Quaternion.identity, 0);
				player4Taken = true;
				this.photonView.RPC ("updatePlayerAvailable", PhotonTargets.All, 4);
				playerPrefabList[3] = PhotonNetwork.player.ID;
			}
		}
	}
	
	public void OnGUI()
	{
		//GUI.Box(new Rect(0, Screen.height-25, 150, 25), "");
		GUILayout.BeginArea(new Rect(0, Screen.height-25, 300, 25));
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button("Return to Lobby"))
		{
			PhotonNetwork.LeaveRoom();  // we will load the menu level when we successfully left the room
		}
		if(showStartGame && PhotonNetwork.player.name.ToLower() == "windows")
		{
			if(GUILayout.Button ("Start Game"))
			{
				showStartGame = false;
				this.photonView.RPC ("startGame", PhotonTargets.All, null);
				song.loop = true;
				song.Play();
			}
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
	}

	[RPC] void startGame()
	{
		GameStartScript.start = true;
	}
	
	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
	}
	
	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		for(int i=0; i<playerPrefabList.Length; i++)
		{
			if(playerPrefabList[i] == PhotonNetwork.player.ID)
			{
				playerPrefabList[i] = 0;
				switch(i)
				{
				case 0: player1Taken = false;
					this.photonView.RPC ("removePlayerAvailable", PhotonTargets.All, 1);
					break;
				case 1: player2Taken = false;
					this.photonView.RPC ("removePlayerAvailable", PhotonTargets.All, 2);
					break;
				case 2: player3Taken = false;
					this.photonView.RPC ("removePlayerAvailable", PhotonTargets.All, 3);
					break;
				case 3: player4Taken = false;
					this.photonView.RPC ("removePlayerAvailable", PhotonTargets.All, 4);
					break;
				}
				break;
			}
		}
		if(PhotonNetwork.playerList.Length == 1)
		{
			GameStartScript.count = 0;
			GameStartScript.start = false;
		}
		// back to main menu        
		Application.LoadLevel(TestMenu.SceneNameMenu);
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		
		// back to main menu        
		Application.LoadLevel(TestMenu.SceneNameMenu);
	}
	
	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);    // you could use this info to store this or react
	}
	
	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}
	
	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}
	
	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		
		// back to main menu        
		Application.LoadLevel(TestMenu.SceneNameMenu);
	}
}
