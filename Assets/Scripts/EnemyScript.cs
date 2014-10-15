using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : Photon.MonoBehaviour
{
	private bool hasSpawn;
	private MoveScript moveScript;
	private EnemyWeaponScript[] weapons;

	private bool alreadySeen = false;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<EnemyWeaponScript>();
		
		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript>();
	}
	
	// 1 - Disable everything
	void Start()
	{
		Spawn ();
		/*hasSpawn = false;
		
		// Disable everything
		// -- collider
		collider2D.enabled = false;
		// -- Moving
		moveScript.enabled = false;
		// -- Shooting
		foreach (EnemyWeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}*/
	}
	
	void Update()
	{
		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false)
		{
			if (renderer.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{
			// Auto-fire
			foreach (EnemyWeaponScript weapon in weapons)
			{
				if (weapon != null && weapon.enabled && weapon.CanAttack)
				{
					weapon.Attack(true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
			
			// 4 - Out of the camera ? Destroy the game object.
			if (renderer.IsVisibleFrom(Camera.main) == false)
			{
				if(alreadySeen && PhotonNetwork.player.name.ToLower() == "windows")
					PhotonNetwork.Destroy(gameObject);
			}
			else
			{
				alreadySeen = true;
			}
		}
	}
	
	// 3 - Activate itself.
	private void Spawn()
	{
		hasSpawn = true;
		
		// Enable everything
		// -- Collider
		collider2D.enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (EnemyWeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}