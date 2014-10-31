using UnityEngine;
using System.Collections;

public class Punch_Powerup_Script : Photon.MonoBehaviour {
	public Transform fist;
	PlayerScript playerScript;

	private bool hasSpawned = false;
	
	// Use this for initialization
	void Start () {
		//Destroy (gameObject, 20);
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){
		playerScript = otherCollider.gameObject.GetComponent<PlayerScript> ();
		if (playerScript != null) {
			Vector3 pos = playerScript.transform.position;
			pos.x = pos.x - 10;
			if(!hasSpawned && PhotonNetwork.player.name.ToLower().Equals ("windows"))
			{
				PhotonNetwork.Instantiate(fist.name, pos, Quaternion.identity, 0);
				hasSpawned = true;
			}
			
			PhotonNetwork.Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	
	void Update () {
		
	}
}
