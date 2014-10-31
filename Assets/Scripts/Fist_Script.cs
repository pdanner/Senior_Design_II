using UnityEngine;
using System.Collections;

public class Fist_Script : Photon.MonoBehaviour {

	public Vector2 speed = new Vector2(2, 2);
	public Vector2 direction = new Vector2(1,0);
	private Vector3 scale;
	EnemyScript enemy;

	private int count = 0;

	// Use this for initialization
	void Start () {
		scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		if(count > 200)
			PhotonNetwork.Destroy(this.gameObject);
		Vector3 fistMovement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
		if (scale.y < 5f) {
			scale.y+=.05f;
			scale.x+=.05f;
			transform.localScale=scale;
		}

		transform.Translate (fistMovement);
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		enemy = otherCollider.gameObject.GetComponent<EnemyScript> ();
		if (enemy != null) {
			Destroy (enemy.gameObject);
		}
	}
}
