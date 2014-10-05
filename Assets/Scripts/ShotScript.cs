using UnityEngine;
using System.Collections;

public class ShotScript : Photon.MonoBehaviour {

	public int damage = 1;
	private int count=0;

	public bool isEnemyShot = false;

	//public PhotonPlayer parent; SCORE_MOD

	// Use this for initialization
	void Start () {

	}

	void Update()
	{
		if (++count == 200)
		{
			Destroy (gameObject);
			//PhotonNetwork.Destroy (gameObject);
		}
	}
}
