using UnityEngine;
using System.Collections;

public class Shield_Powerup_Script : Photon.MonoBehaviour {
	private PlayerScript playerScript;
	private HealthScript healthScript;
	private Attach_Shield_Script attachScript;



	// Use this for initialization
	void Start () {
		//Destroy (gameObject, 20);
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
			playerScript = otherCollider.gameObject.GetComponent<PlayerScript> ();
			if(playerScript != null)
			{
				healthScript = playerScript.GetComponent<HealthScript>();
				attachScript = (Attach_Shield_Script) GameObject.FindObjectOfType(typeof(Attach_Shield_Script));
			}

			attachScript.target = playerScript.transform;
		
			if (playerScript != null) {
				if(attachScript != null)
					attachScript.isActive = 600;
				healthScript.activateShield = 600;
				healthScript.hp = 100;
				PhotonNetwork.Destroy(this.gameObject);
			}
	}
	
	// Update is called once per frame

	void Update () {

	}
}
