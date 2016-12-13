using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip DieClip;
	public AudioClip ChangeEnergy;

	[SerializeField]
	int health = 300;

	public int Health {
		get {
			return health;
		}
	}

	public int MAX_HEALTH = 300;

	private int currentWeapon = 0;
	private List<WeaponControls> weapons = new List<WeaponControls> ();

	public bool isEnergyType1;

	public int MAX_ENERGY = 500;

	int energyType1;

	public int EnergyType1 {
		set { 
			energyType1 = value;
		}
		get {
			return energyType1;
		}
	}

	int energyType2;

	public int EnergyType2 {
		set { 
			energyType2 = value;
		}
		get {
			return energyType2;
		}
	}

	int regenerationRate = 20;
	// regen per second
	float timeCounterForRegen = 0;

	public bool IsDead = false;

	SpriteRenderer sr;

	public void TakeDamage (int damageAmount)
	{
		health -= damageAmount;

		if (health < 0) {
			health = 0;
			killPlayer ();
		}
	}

	public void AddHealth (int amountToAdd)
	{
		if (health + amountToAdd > MAX_HEALTH) {
			health = MAX_HEALTH;
		} else {
			health += amountToAdd;
		}
	}

	public void AddEnergy (int amountToAdd, bool isEnergyType1)
	{

		if (isEnergyType1) {
			if (EnergyType1 + amountToAdd > MAX_ENERGY) {
				EnergyType1 = MAX_ENERGY;
			} else {
				EnergyType1 += amountToAdd;
			}
		
		} else {
			if (EnergyType2 + amountToAdd > MAX_ENERGY) {
				EnergyType2 = MAX_ENERGY;
			} else {
				EnergyType2 += amountToAdd;
			}
		}
	}

	void Start ()
	{
//		Pistol pistol = gameObject.AddComponent <Pistol> ();
//		weapons.Add (pistol);
		EnergyType1 = MAX_ENERGY;
		EnergyType2 = MAX_ENERGY;

		foreach (var item in GetComponentsInChildren<SpriteRenderer>()) {
			if (item.gameObject.name == "ChangeColorLayer") {
				sr = item;
			}
		}

		sr.color = isEnergyType1 ? Color.blue : Color.red;

	}

	void Update ()
	{

		#region inputs
		//cycle down
		if (Input.GetKeyDown (KeyCode.Q)) {
			cycleDownWeapon ();
		}
		//cycle up
		if (Input.GetKeyDown (KeyCode.E)) {
			cycleUpWeapon ();
		}
		//toggle energy type
		if (Input.GetMouseButtonDown(1)) {
			isEnergyType1 = !isEnergyType1;
			sr.color = isEnergyType1 ? Color.blue : Color.red;
			audioSource.clip = ChangeEnergy;
			audioSource.Play();

		}
		#endregion

		regenerateEnergy ();

	}

	void cycleUpWeapon ()
	{
		if (currentWeapon + 1 != weapons.Count) {
			weapons [currentWeapon].enabled = false;
			currentWeapon++;
			weapons [currentWeapon].enabled = true;
		}
	}

	void cycleDownWeapon ()
	{
		if (currentWeapon - 1 >= 0) {
			weapons [currentWeapon].enabled = false;
			currentWeapon--;
			weapons [currentWeapon].enabled = true;
		}
	}

	void killPlayer ()
	{
		audioSource.clip = DieClip;
		audioSource.Play ();
	}


	void regenerateEnergy ()
	{
		timeCounterForRegen += Time.deltaTime;

		if (timeCounterForRegen > 1) {
			timeCounterForRegen = 0;

			if (EnergyType1 + regenerationRate > MAX_ENERGY) {
				EnergyType1 = MAX_ENERGY;
			} else {
				EnergyType1 += regenerationRate;
			}

			if (EnergyType2 + regenerationRate > MAX_ENERGY) {
				EnergyType2 = MAX_ENERGY;
			} else {
				EnergyType2 += regenerationRate;
			}
		}

	}
		
}
