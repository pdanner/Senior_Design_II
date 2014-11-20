using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class HealthScript : Photon.MonoBehaviour {

	public int hp;

	public bool isEnemy = true;

	public Transform shield_powerup; // TG
	public Transform speed_powerup;
	public Transform punch_powerup;

	public int range;
	public int activateShield = 0; // TG

	public static int multiplier = 1;
	public const string PlayerHealthProp = "health";
	private int finalScore = 0;

	public static int numAlive = 0;

	void Awake()
	{
			SetHealth (PhotonNetwork.player, hp);
	}

	public void Damage()
	{
		if(this.photonView.isMine)
		{
			if (activateShield <= 0) 
			{
				SetHealth (PhotonNetwork.player, --hp);
			}
			if (GetHealth (PhotonNetwork.player) <= 0)
			{
				photonView.RPC ("destroyObject", PhotonTargets.All, null);
			}
		}
	}

	[RPC] void removePlayer()
	{
		numAlive--;
		if(numAlive <= 0)
		{
			PhotonNetwork.DestroyAll();
			PhotonNetwork.LoadLevel("GameOver");
		}
	}

	[RPC] void destroyObject()
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
			//Application.LoadLevel ("GameOver");
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
					shot.isEnemyShot = true;
					//Debug.Log(shot.parent.name);
					Damage();

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
					Damage();
					
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
			activateShield--;
			}
	} // TG

	public static void SetHealth(PhotonPlayer player, int hp)
	{
		Hashtable health = new Hashtable();  // using PUN's implementation of Hashtable
		health[PlayerHealthProp] = hp;
		
		player.SetCustomProperties(health);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static void UpdateHealth(PhotonPlayer player)
	{
		int current = GetHealth (player);
		Hashtable health = new Hashtable();  // using PUN's implementation of Hashtable
		health[PlayerHealthProp] = current - 1;
		
		player.SetCustomProperties(health);
	}
		
	public static int GetHealth(PhotonPlayer player)
	{
		object teamId;
		if (player.customProperties.TryGetValue(PlayerHealthProp, out teamId))
		{
			return (int)teamId;
		}
		
		return 0;
	}
}
