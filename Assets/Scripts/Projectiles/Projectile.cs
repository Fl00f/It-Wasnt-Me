using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	[SerializeField]
	protected int amountOfDamage = 1;

	protected float projectileSpeed = 10;
	public bool isEnergyType1 = false;

	static Vector3 center = new Vector3(0,0,0);

	protected void moveForward (){
		transform.position += transform.forward * Time.deltaTime * projectileSpeed;
	}

	protected void Start(){
		GetComponentInChildren<SpriteRenderer> ().color = isEnergyType1 ? Color.blue : Color.red;
		GetComponentInChildren<SpriteRenderer> ().enabled = true;
	}

	void OnCollisionEnter (Collision collision){

		if (collision.gameObject.GetComponent<EnemyBase>()) {
			collision.gameObject.GetComponent<EnemyBase> ().TakeDamage (amountOfDamage, isEnergyType1);
		}


		Destroy (gameObject);
	}

	protected void distanceCheck(){
		if (Vector3.Distance(transform.position, center) > 20) {
			Destroy (gameObject);
		}

	}
}
