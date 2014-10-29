using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class GuiTextFollower : Photon.MonoBehaviour {
	
	public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
	public float clampBorderSize = 0.05f;  // How much viewport space to leave at the borders when a label is being clamped
	public bool useMainCamera = true;   // Use the camera tagged MainCamera
	public Camera cameraToUse ;   // Only use this if useMainCamera is false
	private PlayerScript player;
	Camera cam ;
	Transform thisTransform;
	Transform camTransform;
	
	void Start () 
	{
		if (useMainCamera)
			cam = Camera.main;
		else
			cam = cameraToUse;
		camTransform = cam.transform;
	}
	
	
	void Update()
	{
		if(!PhotonNetwork.playerName.ToLower().Equals ("windows"))
		{
			this.transform.guiText.text = PlayerScript.GetAmmo (PhotonNetwork.player).ToString();
			this.transform.position = new Vector3 (0.1f, 0.9f, 0f);
		}
		else
		{
			this.transform.guiText.text = "";
		}
		
	}
}