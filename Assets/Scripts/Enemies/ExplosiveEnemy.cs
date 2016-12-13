using UnityEngine;
using System.Collections;

public class ExplosiveEnemy : EnemyBase {

	void Start () {
		base.start ();
		damageAmount = 20;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.GetComponent<Player>()) {
			collision.gameObject.GetComponent<Player> ().TakeDamage (damageAmount);
			FindObjectOfType<GameMaster> ().EnemyDieSource.Play ();
			DestroySelf ();
		}
	}
}
