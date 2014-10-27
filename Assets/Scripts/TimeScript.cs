using UnityEngine;
using System.Collections;

public class TimeScript : Photon.MonoBehaviour {
	
	private float currentTime;
	
	public PhotonPlayer parent; //SCORE_MOD

	private float startTime;
	
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	void Update()
	{
		//Game is running
		if (PlayerScript.start == true) 
		{
			currentTime = Time.time;
		}
		//Game is not running
		//Keep track of time until we need to start counting again
		else
		{
			startTime = Time.time;
		}
		if (currentTime - startTime <= 20f) 
		{
			HealthScript.multiplier = 1;
		}
		else if (currentTime - startTime <= 40f) 
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
