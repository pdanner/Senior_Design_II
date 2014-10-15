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
			photonView.RPC ("destroyObject", PhotonTargets.All, null);
		}
	}

	[RPC] void destroyObject()
	{
		if(!gameObject.name.Contains("enemy") && !gameObject.name.Contains ("boss"))
		{
			//Player
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();
			//Each player will PhotonNetwork.Destroy their own GO
			if(this.photonView.isMine)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}
		else
		{
			//Enemy or Boss
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();
			//Windows spawned the Enemies, so they have to destroy them
			if(PhotonNetwork.player.name.ToLower() == "windows")	
				PhotonNetwork.Destroy(gameObject);
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

					//Debug.Log(shot.parent.name);
					Damage(shot.damage);

					shot.parent.AddScore(1); //SCORE_MOD
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
