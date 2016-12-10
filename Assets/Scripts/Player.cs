using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	[SerializeField]
	int health = 300;
	public int Health {
		get {
			return health;
		}
	}

	private int currentWeapon = 0;
	private List<WeaponControls> weapons = new List<WeaponControls>();

	public bool isEnergyType1;

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

	public void AddHealth(int amountToAdd){
		health += amountToAdd;
	}

	void Start(){
		Pistol pistol = gameObject.AddComponent <Pistol>();
		weapons.Add (pistol);
	}

	void Update(){

		#region inputs
		//cycle down
		if (Input.GetKeyDown(KeyCode.Q)) {
			cycleDownWeapon();
		}
		//cycle up
		if (Input.GetKeyDown(KeyCode.E)) {
			cycleUpWeapon();
		}
		//toggle energy type
		if (Input.GetKeyDown(KeyCode.Tab)) {
			isEnergyType1 = !isEnergyType1;
		}
		#endregion

	}

	void cycleUpWeapon(){
		if (currentWeapon+1 != weapons.Count) {
			weapons [currentWeapon].enabled = false;
			currentWeapon++;
			weapons [currentWeapon].enabled = true;
		}
	}

	void cycleDownWeapon(){
		if (currentWeapon-1 >=0) {
			weapons [currentWeapon].enabled = false;
			currentWeapon--;
			weapons [currentWeapon].enabled = true;
		}
	}

	void killPlayer(){

	}
		
}
