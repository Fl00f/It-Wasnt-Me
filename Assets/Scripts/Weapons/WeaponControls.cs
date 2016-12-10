﻿using UnityEngine;
using System.Collections;

public abstract class WeaponControls : MonoBehaviour {
	public GameObject projectilePrefab;
	protected string projectilePrefabName = "projectile";
	protected string projectilePrefabLocation = "Prefabs/Projectiles/";
	Player player;
	protected float fireRatePerSecond = 5f; //per second
	protected float fireCounter = 0;
	protected bool canShoot = true;

	protected void FireWeapon (){
		GameObject proj = Instantiate (projectilePrefab) as GameObject;
		proj.GetComponent<Projectile> ().isEnergyType1 = player.isEnergyType1;
		proj.transform.position = transform.position;
		proj.transform.rotation = transform.rotation;
	}

	protected float calculatedFireRate(){
		return 1f / fireRatePerSecond;
	}

	protected void Start(){
		player = GetComponent<Player> ();
	}

	protected void loadProjectilePrefab(){
		projectilePrefab = Resources.Load (projectilePrefabLocation+projectilePrefabName) as GameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			if (canShoot) {
				FireWeapon ();
				canShoot = false;
			} else {
				fireCounter += Time.deltaTime;
				if (fireCounter > calculatedFireRate ()) {
					canShoot = true;
					fireCounter = 0f;
				}
			}

		}
	}
}
