using UnityEngine;
using System.Collections;

[RequireComponent (typeof (GUIText))]
public class ScoreTextScript : Photon.MonoBehaviour {
	
	public bool clampToScreen = false;  // If true, label will be visible even if object is off screen
	public float clampBorderSize = 0.05f;  // How much viewport space to leave at the borders when a label is being clamped
	public bool useMainCamera = true;   // Use the camera tagged MainCamera
	public Camera cameraToUse ;   // Only use this if useMainCamera is false
	private PlayerScript player;
	Camera cam ;
	Transform thisTransform;
	Transform camTransform;

	private float dist;
	private float topBorder;
	private float rightBorder;
	private float bottomBorder;
	
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

		dist = (transform.position - Camera.main.transform.position).z;
		
		rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;

		if(!PhotonNetwork.playerName.ToLower().Equals ("windows"))
		{
			this.transform.guiText.text = PhotonNetwork.player.GetScore().ToString();
			this.transform.position = new Vector3 (0.9f, 0.9f, 0f);
		}
		else
		{
			this.transform.guiText.text = "";
		}



//		if (clampToScreen)
//		{
//			Vector3 relativePosition = camTransform.InverseTransformPoint(target.position);
//			relativePosition.z =  Mathf.Max(relativePosition.z, 1.0f);
//			thisTransform.position = cam.WorldToViewportPoint(camTransform.TransformPoint(relativePosition + offset));
//			thisTransform.position = new Vector3(Mathf.Clamp(thisTransform.position.x, clampBorderSize, 1.0f - clampBorderSize),
//			                                     Mathf.Clamp(thisTransform.position.y, clampBorderSize, 1.0f - clampBorderSize),
//			                                     thisTransform.position.z);
//			
//		}
//		else
//		{
//			thisTransform.position = cam.WorldToViewportPoint(target.position + offset);
//		}
	}
}
