using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	[SerializeField]
	int health = 300;
	public int Health {
		get {
			return health;
		}
	}




	bool isEnergyType1;

	[SerializeField]
	int energyType1;
	public int EnergyType1 {
		get {
			return energyType1;
		}
	}

	[SerializeField]
	int energyType2;
	public int EnergyType2 {
		get {
			return energyType2;
		}
	}

	public void TakeDamage(int damageAmount){
		health -= damageAmount;

		if (health <0) {
			health = 0;
			killPlayer ();
		}
	}



	void switchWeapon(){
	}

	void killPlayer(){

	}
}
