using UnityEngine;
using System.Collections;

public class Pistol : WeaponControls
{

	#region implemented abstract members of WeaponControls

	public override void FireWeapon ()
	{
		GameObject proj = Instantiate (projectilePrefab) as GameObject;
		proj.transform.position = transform.position;
		proj.transform.rotation = transform.rotation;
	}

	#endregion

	void Start ()
	{
		fireRatePerSecond = 5f;
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
