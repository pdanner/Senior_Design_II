using UnityEngine;
using System.Collections;

public class WeaponScript : Photon.MonoBehaviour
{
	public Transform shotPrefab;

	public float shootingRate = 0.25f;

	public float shootCooldown;
	
	void Start()
	{
		shootCooldown = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	// Fire off a round!
	[RPC]
	void FireOneShot()
	{
		GameObject shotTransform = Instantiate (Resources.Load("Shot"), transform.position, transform.rotation) as GameObject;
		//shotTransform.position = transform.position + new Vector3(4f, -0.3f, 0);
		ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			shot.isEnemyShot = false; //	isEnemy;
		}
		
		MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
		if (move != null)
		{
			move.direction = this.transform.right; 
		}
	}
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;
			this.photonView.RPC ("FireOneShot", PhotonTargets.All, null);
			//var shotTransform = Instantiate(shotPrefab) as Transform;

			//shotTransform.position = transform.position + new Vector3(4f, -0.3f, 0);

			//
		}
	}

	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
}