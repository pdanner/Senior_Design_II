using UnityEngine;
using System.Collections;

public class SpawnEnemy : Photon.MonoBehaviour {

	public Transform enemyPrefab;

	private int spawn = 0;

	// Use this for initialization
	void Start () {
		//MoveScript move = enemyPrefab.gameObject.GetComponent<MoveScript> ();
		//move.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(PhotonNetwork.playerName.ToLower() == "windows")
		{
			spawn = Random.Range (0, 100);
			if(spawn == 1)
			{
				this.photonView.RPC ("Spawn", PhotonTargets.All, null);
			}
		}
	}
	[RPC] void Spawn()
	{
		var dist = (transform.position - Camera.main.transform.position).z;
		
		var rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		var topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		var bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;

		GameObject enemyTransform = Instantiate (Resources.Load("enemy"), new Vector3(rightBorder - 5f, Random.Range (bottomBorder, topBorder), 0), 
		                                        transform.rotation) as GameObject;
	}
}
