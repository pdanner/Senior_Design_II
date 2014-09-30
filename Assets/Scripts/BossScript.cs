﻿using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class BossScript : MonoBehaviour
{
	private bool hasSpawn;
	private MoveScript moveScript;
	private BossWeaponScript[] weapons;
	
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<BossWeaponScript>();
		
		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<MoveScript>();
	}
	
	// 1 - Disable everything
	void Start()
	{
		hasSpawn = false;
		
		// Disable everything
		// -- collider
		collider2D.enabled = false;
		// -- Moving
		moveScript.enabled = false;
		// -- Shooting
		foreach (BossWeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
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
			foreach (BossWeaponScript weapon in weapons)
			{
				if (weapon != null && weapon.enabled && (weapon.CanAttack || weapon.CanAttack2 || weapon.CanAttack3))
				{
					weapon.Attack(true);
					SoundEffectsHelper.Instance.MakeEnemyShotSound();
				}
			}
			
			// 4 - Out of the camera ? Destroy the game object.
			if (renderer.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
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
		foreach (BossWeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}

	void OnDestroy()
	{
		// Game Over.
		// Add the script to the parent because the current game
		// object is likely going to be destroyed immediately.
		transform.parent.gameObject.AddComponent<WinningScript>();
	}
}