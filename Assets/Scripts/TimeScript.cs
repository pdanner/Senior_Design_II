using UnityEngine;
using System.Collections;

public class TimeScript : Photon.MonoBehaviour {
	
	private float currentTime;
	
	public PhotonPlayer parent; //SCORE_MOD
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update()
	{
		if (PlayerScript.start == true) 
		{
			currentTime = Time.time;
		}
		if (currentTime <= 20) 
		{
			HealthScript.multiplier = 1;
		}
		else if (currentTime <= 40) 
		{
			HealthScript.multiplier = 2;
		}
		else
		{
			HealthScript.multiplier = 4;
		}
		//Debug.Log ("Time and Multi " + currentTime + HealthScript.multiplier);
	}
}
