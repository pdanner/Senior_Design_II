using UnityEngine;
using System.Collections;

public class ShotScript : Photon.MonoBehaviour {

	public int damage = 1;
	private int count=0;

	public bool isEnemyShot = false;

	// Use this for initialization
	void Start () {
		//PhotonNetwork.Instantiate("Shot", transform.position, Quaternion.identity, 0);
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
