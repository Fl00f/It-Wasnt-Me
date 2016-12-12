using UnityEngine;
using System.Collections;

public class Pistol : WeaponControls
{
	
	void Start ()
	{
		fireRatePerSecond = 10f;
		projectilePrefabName = "projectile";
		loadProjectilePrefab ();
		WeaponNozzleOffSet = new Vector3 (0.636f, 0f, 1.7f);
		base.Start ();
	}

}
