using UnityEngine;
using System.Collections;

public class Pistol : WeaponControls
{
	
	void Start ()
	{
		fireRatePerSecond = 10f;
		projectilePrefabName = "projectile";
		loadProjectilePrefab ();
		base.Start ();
	}

}
