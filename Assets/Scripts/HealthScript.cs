using UnityEngine;
using System.Collections;

public class HealthScript : Photon.MonoBehaviour {

	public int hp = 2;

	public bool isEnemy = true;

	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Player Shot
			if(shot.isEnemyShot == false)
			{
				// Shooting Enemy
				if(isEnemy)
				{
					//shot.parent.AddScore(1); SCORE_MOD

					Damage(shot.damage);
					
					// Destroy the shot
					Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
				}
			}
			// Enemy Shot
			else
			{
				// Player got hit
				if(!isEnemy)
				{
					Damage(shot.damage);
					
					// Destroy the shot
					Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
				}
			}

			// Avoid friendly fire
//			if (shot.isEnemyShot != isEnemy)
//			{
//				Damage(shot.damage);
//				
//				// Destroy the shot
//				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
//			}
}
	}}
