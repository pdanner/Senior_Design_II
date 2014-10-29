using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerScript : Photon.MonoBehaviour {
	
	public Vector2 speed = new Vector2(15, 15);
	private float calibration = 0.7f;
	//public int ammo = 12;
	private int frameCount = 0;
	
	public int moveSpeed = 10; // TG
	public int activateSpeed = 0;
	
	public const string PlayerAmmoProp = "ammo";
	
	public static bool start = false;
	
	void Awake()
	{
		start = false;
		SetAmmo (PhotonNetwork.player);
	}
	
	void Update () {
		if(start)
		{
			if(this.photonView.isMine)
			{
				Vector3 dir = Vector3.zero;
				
				dir.x += Input.acceleration.x * speed.x;
				dir.y += (calibration + Input.acceleration.y) * speed.y;
				
				if (dir.sqrMagnitude > 1) {
					dir.Normalize ();
				}
				
				if(activateSpeed > 0){ // TG
					dir *= Time.deltaTime*moveSpeed;
					activateSpeed--;
				}
				else{
					dir *= Time.deltaTime*10;
				} // TG
				
				transform.Translate (dir);
				
				bool shoot = Input.GetButton ("Fire1");
				shoot |= Input.GetButton ("Fire2");
				if (shoot) {
					WeaponScript weapon = GetComponent<WeaponScript> ();
					weapon.parent = PhotonNetwork.player; //SCORE_MOD
					if (Input.touchCount == 2) {
						if (GetAmmo (PhotonNetwork.player) != 12) {
							ReloadAmmo (PhotonNetwork.player);
							weapon.shootCooldown = 2.0f;
							SoundEffectsHelper.Instance.MakePlayerReloadSound ();
						}
					} else {
						if (GetAmmo (PhotonNetwork.player) <= 0) {
							weapon.shootCooldown = 2.0f;
							ReloadAmmo (PhotonNetwork.player);
							SoundEffectsHelper.Instance.MakePlayerReloadSound ();
						}
						if (weapon != null && weapon.shootCooldown <= 0 && GetAmmo (PhotonNetwork.player) > 0) {
							weapon.Attack (false);
							ShootAmmo (PhotonNetwork.player);
							SoundEffectsHelper.Instance.MakePlayerShotSound ();
						}
					}
					//					if (Input.touchCount == 2) {
					//						if (ammo != 12) {
					//							ammo = 12;
					//							weapon.shootCooldown = 2.0f;
					//							SoundEffectsHelper.Instance.MakePlayerReloadSound ();
					//						}
					//					} else {
					//						if (ammo <= 0) {
					//							weapon.shootCooldown = 2.0f;
					//							ammo = 12;
					//							SoundEffectsHelper.Instance.MakePlayerReloadSound ();
					//						}
					//						if (weapon != null && weapon.shootCooldown <= 0 && ammo > 0) {
					//							weapon.Attack (false);
					//							ammo--;
					//							SoundEffectsHelper.Instance.MakePlayerShotSound ();
					//						}
					//					}
				}
				var dist = (transform.position - Camera.main.transform.position).z;
				
				var leftBorder = Camera.main.ViewportToWorldPoint(
					new Vector3(0, 0, dist)
					).x;
				
				var rightBorder = Camera.main.ViewportToWorldPoint(
					new Vector3(1, 0, dist)
					).x;
				
				var topBorder = Camera.main.ViewportToWorldPoint(
					new Vector3(0, 0, dist)
					).y;
				
				var bottomBorder = Camera.main.ViewportToWorldPoint(
					new Vector3(0, 1, dist)
					).y;
				
				transform.position = new Vector3(
					Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
					Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
					transform.position.z
					);
			}
			
		}
		//frameCount++;
	}
	
	void OnDestroy()
	{
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		//transform.parent.gameObject.AddComponent<GameOverScript>();
	}
	
	public static void SetAmmo(PhotonPlayer player)
	{
		Hashtable ammo = new Hashtable();  // using PUN's implementation of Hashtable
		ammo[PlayerAmmoProp] = 12;
		
		player.SetCustomProperties(ammo);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static void ShootAmmo(PhotonPlayer player)
	{
		int current = GetAmmo (player);
		Hashtable ammo = new Hashtable();  // using PUN's implementation of Hashtable
		ammo[PlayerAmmoProp] = current - 1;
		
		player.SetCustomProperties(ammo);
	}
	
	public static void ReloadAmmo(PhotonPlayer player)
	{
		Hashtable ammo = new Hashtable();  // using PUN's implementation of Hashtable
		ammo[PlayerAmmoProp] = 12;
		
		player.SetCustomProperties(ammo);  // this locally sets the score and will sync it in-game asap.
	}
	
	public static int GetAmmo(PhotonPlayer player)
	{
		object teamId;
		if (player.customProperties.TryGetValue(PlayerAmmoProp, out teamId))
		{
			return (int)teamId;
		}
		
		return 0;
	}
}