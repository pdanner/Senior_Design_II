// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkerMenu.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestMenu : MonoBehaviour
{
	private string roomName = "myRoom";
	
	private Vector2 scrollPos = Vector2.zero;
	
	private bool connectFailed = false;
	
	public static readonly string SceneNameMenu = "Menu";
	
	public static readonly string SceneNameGame = "Battleground";

	private GUISkin skin;

	public AudioSource song;

	public bool isWindows = false;
	
	private string errorDialog;
	private double timeToClearDialog;
	public string ErrorDialog
	{
		get 
		{ 
			return errorDialog; 
		}
		private set
		{
			errorDialog = value;
			if (!string.IsNullOrEmpty(value))
			{
				timeToClearDialog = Time.time + 4.0f;
			}
		}
	}
	
	public void Awake()
	{

		skin = Resources.Load ("GUISkin2") as GUISkin;
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;
		
		// the following line checks if this client was just created (and not yet online). if so, we connect
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			// Connect to the photon master-server. We use the settings saved in PhotonServerSettings (a .asset file in this project)
			PhotonNetwork.ConnectUsingSettings("0.9");
		}
		if(isWindows)
		{
			PhotonNetwork.playerName = "windows";
			PhotonNetwork.player.name = "windows";
		}
		else
		{
			PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
		}
		if(PhotonNetwork.playerName.ToLower().Equals ("windows"))
		{
			song.loop = true;
			song.Play();
		}
	}
	
	public void OnGUI()
	{
		skin = Resources.Load ("GUISkinHost") as GUISkin;
		GUI.skin = skin;
		GameStartScript.start = false;
		if (!PhotonNetwork.connected)
		{
			if (PhotonNetwork.connecting)
			{
				GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress);
			}
			else
			{
				GUILayout.Label("Not connected. Check console output. Detailed connection state: " + PhotonNetwork.connectionStateDetailed + " Server: " + PhotonNetwork.ServerAddress);
			}
			
			if (this.connectFailed)
			{
				GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
				GUILayout.Label(String.Format("Server: {0}", new object[] {PhotonNetwork.ServerAddress}));
				GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID);
				
				if (GUILayout.Button("Try Again", GUILayout.Width(100)))
				{
					this.connectFailed = false;
					PhotonNetwork.ConnectUsingSettings("0.9");
				}
			}
			
			return;
		}
		if(isWindows)
		{
			skin = Resources.Load ("GUISkinHost") as GUISkin;
			GUI.skin = skin;
			if (GUI.Button(	new Rect(3*(Screen.width / 5) - (100), (Screen.height * 2/5), 250,125), ""))
			{
				// Host Windows
				PhotonNetwork.playerName = "windows";
				PhotonNetwork.player.name = "windows";
				PhotonNetwork.CreateRoom(this.roomName, new RoomOptions() { maxPlayers = 10 }, null);
			}
		}
		else
		{
			skin = Resources.Load ("GUISkinJoin") as GUISkin;
			GUI.skin = skin;
			if (GUI.Button(	new Rect(3*(Screen.width / 5) - (100), (Screen.height * 2/5), 250,125), ""))
			{
				// Load Pick A Player Screen 
				Application.LoadLevel("PickAPlayer");
			}
		}

		skin = Resources.Load ("GUISkinAbout") as GUISkin;
		GUI.skin = skin;
		if (GUI.Button(	new Rect(3*(Screen.width / 5) - (100), (Screen.height * 3/5), 250,125), ""))
		{
			// About the Team
			Application.LoadLevel ("About");
		}
		skin = Resources.Load ("GUISkinExit") as GUISkin;
		GUI.skin = skin;
		if (GUI.Button(	new Rect(3 *(Screen.width / 5) - (100), (Screen.height * 4/5), 250, 125), ""))
		{
			// Quit
			Application.Quit();
		}
	}
	
	// We have two options here: we either joined(by title, list or random) or created a room.
	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
	}
	
	
	public void OnPhotonCreateRoomFailed()
	{
		this.ErrorDialog = "Error: Can't create room (room name maybe already used).";
		Debug.Log("OnPhotonCreateRoomFailed got called. This can happen if the room exists (even if not visible). Try another room name.");
	}
	
	public void OnPhotonJoinRoomFailed()
	{
		this.ErrorDialog = "Error: Can't join room (full or unknown room name).";
		Debug.Log("OnPhotonJoinRoomFailed got called. This can happen if the room is not existing or full or closed.");
	}
	public void OnPhotonRandomJoinFailed()
	{
		this.ErrorDialog = "Error: Can't join random room (none found).";
		Debug.Log("OnPhotonRandomJoinFailed got called. Happens if no room is available (or all full or invisible or closed). JoinrRandom filter-options can limit available rooms.");
	}
	
	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		PhotonNetwork.LoadLevel(SceneNameGame);
		PhotonNetwork.automaticallySyncScene = true;
	}
	
	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
	}
	
	public void OnFailedToConnectToPhoton(object parameters)
	{
		this.connectFailed = true;
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}
}

