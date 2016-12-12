using UnityEngine;
using System.Collections;

public class PistolBullet : Projectile {

	// Use this for initialization
	void Start () {
		amountOfDamage = 30;
		projectileSpeed = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Start ();
		moveForward ();
		distanceCheck ();
	}


}
