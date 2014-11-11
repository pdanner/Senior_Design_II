using UnityEngine;
using System.Collections;

public class Speed_Powerup_Script : Photon.MonoBehaviour {
	private PlayerScript playerScript;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 20);
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		playerScript = otherCollider.gameObject.GetComponent<PlayerScript> ();
		if (playerScript != null) {
			playerScript.moveSpeed = 15;
			playerScript.activateSpeed = 600;
			PhotonNetwork.Destroy(this.gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
