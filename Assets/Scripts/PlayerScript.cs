using UnityEngine;
using System.Collections;

public class PlayerScript : Photon.MonoBehaviour {
	
	public Vector2 speed = new Vector2(15, 15);
	private float calibration = 0.7f;
	public int ammo = 12;
	private int frameCount = 0;
	// Update is called once per frame

	void Update () {
		if(frameCount > 180)
		{
			if(this.photonView.isMine)
			{
				Vector3 dir = Vector3.zero;
				
				dir.x += Input.acceleration.x * speed.x;
				dir.y += (calibration + Input.acceleration.y) * speed.y;
				
				if (dir.sqrMagnitude > 1) {
					dir.Normalize ();
				}
				
				dir *= Time.deltaTime*10;
				
				transform.Translate (dir);
				
				bool shoot = Input.GetButton ("Fire1");
				shoot |= Input.GetButton ("Fire2");
				if (shoot) {
					WeaponScript weapon = GetComponent<WeaponScript> ();
					if (Input.touchCount == 2) {
						if (ammo != 12) {
							ammo = 12;
							weapon.shootCooldown = 2.0f;
							SoundEffectsHelper.Instance.MakePlayerReloadSound ();
						}
					} else {
						if (ammo <= 0) {
							weapon.shootCooldown = 2.0f;
							ammo = 12;
							SoundEffectsHelper.Instance.MakePlayerReloadSound ();
						}
						if (weapon != null && weapon.shootCooldown <= 0 && ammo > 0) {
							weapon.Attack (false);
							ammo--;
							SoundEffectsHelper.Instance.MakePlayerShotSound ();
						}
					}
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
		frameCount++;
	}
	
	void OnDestroy()
	{
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		// transform.parent.gameObject.AddComponent<GameOverScript>();
	}
}