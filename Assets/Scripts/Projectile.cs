using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	[SerializeField]
	int amountOfDamage = 1;

	float projectileSpeed = 10;

	protected void moveForward (){
		transform.position += transform.forward * Time.deltaTime * projectileSpeed;
	}
		
}
