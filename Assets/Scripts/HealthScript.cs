using UnityEngine;
using System.Collections;

public class HealthScript : Photon.MonoBehaviour {

	public int hp = 2;

	public bool isEnemy = true;

	public Transform shield_powerup; // TG
	public Transform speed_powerup;
	public int range;
	public int activateShield = 0; // TG

	public static int multiplier = 1;

	private int finalScore = 0;

	public static int numAlive = 0;

	public void Damage(int damageCount)
	{
		hp -= damageCount;
		
		if (hp <= 0)
		{
			photonView.RPC ("destroyObject", PhotonTargets.All, null);
		}
	}

	[RPC] void removePlayer()
	{
		numAlive--;
		if(numAlive == 0)
		{
			PhotonNetwork.DestroyAll();
			PhotonNetwork.LoadLevel("GameOver");
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
				PlayerScript.start = false;
				PhotonNetwork.Destroy(gameObject);
				//PhotonNetwork.LeaveRoom();
				photonView.RPC ("removePlayer", PhotonTargets.MasterClient, null);
				Application.LoadLevel ("GameOver");
			}
		}
		else if(gameObject.name.Contains("enemy"))
		{
			//Enemy
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();

			range = Random.Range(1,30); // TG

			//Windows spawned the Enemies, so they have to destroy them
			if(PhotonNetwork.player.name.ToLower() == "windows")
			{
				if(range == 1){
					PhotonNetwork.Instantiate(shield_powerup.name, transform.position, Quaternion.identity, 0);
				}
				else if(range == 2){
					PhotonNetwork.Instantiate(speed_powerup.name, transform.position, Quaternion.identity, 0);
				}
				PhotonNetwork.Destroy(gameObject);
			}
		}
		else if (gameObject.name.Contains ("boss"))
		{
			//Time.timeScale = 0.3f; //Slow mo finish
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();
			if(PhotonNetwork.player.name.ToLower() == "windows")
			{
				PhotonNetwork.DestroyAll();
				PhotonNetwork.LoadLevel("GameWin");
			}
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

					//if(this.photonView.isMine)
					shot.parent.AddScore(1 * multiplier); //SCORE_MOD

					// Destroy the shot if it is mine
					PhotonNetwork.Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
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

		}
	}

	void Update() //TG
	{
		//600 = 10 seconds
		if (activateShield > 0) {
			hp = 100;
			activateShield--;
			if(activateShield == 0){
				this.hp = 4; // TODO: Fix players HP that they return to
			}
		}
	} // TG
}
