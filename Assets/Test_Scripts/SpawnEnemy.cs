﻿using UnityEngine;
using System.Collections;

public class SpawnEnemy : Photon.MonoBehaviour {

	public Transform enemyPrefab;
	public Transform bossPrefab;

	private int spawn = 0;

	private int timer = 0;

	private bool spawnedBoss = false;

	private bool canSpawn = false;

	private float dist;
	private float topBorder;
	private float rightBorder;
	private float bottomBorder;

	// Use this for initialization
	void Start () {
		//MoveScript move = enemyPrefab.gameObject.GetComponent<MoveScript> ();
		//move.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

		dist = (transform.position - Camera.main.transform.position).z;
		
		rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, dist)
			).x;
		
		topBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, dist)
			).y;
		
		bottomBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 1, dist)
			).y;
		if(GameStartScript.count  > 180)
		{
			canSpawn = true;
			timer++;
		}
		if(PhotonNetwork.playerName.ToLower() == "windows")
		{
			if(timer < 5*60) // 30 seconds
			{
				if(canSpawn)
					spawn = Random.Range (0, 150);
				else
					spawn = 0;
				if(spawn == 1)
				{
					float spawnY = Random.Range (bottomBorder-1f, topBorder+1f);
					float spawnX = rightBorder + 5f;

					//this.photonView.RPC ("Spawn", PhotonTargets.All, spawnX, spawnY);
					GameObject enemyTransform = PhotonNetwork.Instantiate (enemyPrefab.name, new Vector3(spawnX, spawnY, 0f), 
					                                                       transform.rotation, 0) as GameObject;
				}
			}
			else if(timer > 7*60)
			{
				if(!spawnedBoss && canSpawn)
				{
					float spawnX = rightBorder;
					GameObject bossTransform = PhotonNetwork.Instantiate (bossPrefab.name, new Vector3(spawnX, 0f, 0f), 
					                                                      transform.rotation, 0) as GameObject;
					spawnedBoss = true;
				}
			}
		}
	}
//	[RPC] void Spawn(float x, float y)
//	{
//		GameObject enemyTransform = PhotonNetwork.Instantiate (enemyPrefab.name, new Vector3(x, y, 0f), 
//		                                        transform.rotation, 0) as GameObject;
//	}
}
