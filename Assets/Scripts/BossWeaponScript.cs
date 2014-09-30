using UnityEngine;
using System.Collections;

public class BossWeaponScript : MonoBehaviour
{
	public Transform shotPrefab;

	public Transform shot2Prefab;

	public Transform shot3Prefab;
	
	public float shootingRate = 0.25f;

	public float shootingRate2 = 0.25f;

	public float shootingRate3 = 0.25f;
	
	public float shootCooldown;
	public float shootCooldown2;
	public float shootCooldown3;
	
	void Start()
	{
		shootCooldown = 0f;
		shootCooldown2 = 0f;
		shootCooldown3 = 0f;
	}
	
	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
		if (shootCooldown2 > 0) 
		{
			shootCooldown2 -= Time.deltaTime;
		}
		if (shootCooldown3 > 0) 
		{
			shootCooldown3 -= Time.deltaTime;
		}
	}
	
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			//First weapon
			var shotTransform = Instantiate(shotPrefab) as Transform;
			
			shotTransform.position = transform.position + new Vector3(-7.5f, 5.0f, 0);
			
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}
			
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			if (move != null)
			{
				move.direction = this.transform.right; 
			}

		}
		if (CanAttack2) 
		{
			shootCooldown2 = shootingRate2;
			
			//Second weapon
			var shot2Transform = Instantiate(shot2Prefab) as Transform;
			
			shot2Transform.position = transform.position + new Vector3(-7f, 0, 0);
			
			ShotScript shot2 = shot2Transform.gameObject.GetComponent<ShotScript>();
			if (shot2 != null)
			{
				shot2.isEnemyShot = isEnemy;
			}
			
			MoveScript move2 = shot2Transform.gameObject.GetComponent<MoveScript>();
			if (move2 != null)
			{
				move2.direction = this.transform.right; 
			}
		}
		if (CanAttack3) 
		{
			shootCooldown3 = shootingRate3;

			//Second weapon
			var shot3Transform = Instantiate(shot3Prefab) as Transform;
			
			shot3Transform.position = transform.position + new Vector3(-1f, -5f, 0);
			
			ShotScript shot3 = shot3Transform.gameObject.GetComponent<ShotScript>();
			if (shot3 != null)
			{
				shot3.isEnemyShot = isEnemy;
			}
			
			MoveScript move3 = shot3Transform.gameObject.GetComponent<MoveScript>();
			if (move3 != null)
			{
				move3.direction = this.transform.right; 
			}
		}

		
	}
	
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
	public bool CanAttack2
	{
		get
		{
			return shootCooldown2 <= 0f;
		}
	}
	public bool CanAttack3
	{
		get
		{
			return shootCooldown3 <= 0f;
		}
	}
}