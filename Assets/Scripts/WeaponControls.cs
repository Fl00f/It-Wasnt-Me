using UnityEngine;
using System.Collections;

public abstract class WeaponControls : MonoBehaviour {
	public GameObject projectilePrefab;

	protected float fireRatePerSecond = 5f; //per second
	protected float fireCounter = 0;
	protected bool canShoot = true;

	public abstract void FireWeapon ();
	protected float calculatedFireRate(){
		return 1f / fireRatePerSecond;
	}
}
