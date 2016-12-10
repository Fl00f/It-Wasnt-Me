using UnityEngine;
using System.Collections;

public class PistolBullet : Projectile {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		moveForward ();
	}

	void OnCollisionEnter (Collision collision){




		Destroy (gameObject);
	}
}
