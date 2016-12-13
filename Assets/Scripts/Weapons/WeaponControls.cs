using UnityEngine;
using System.Collections;

public abstract class WeaponControls : MonoBehaviour {
	public AudioSource audioSource;
	public AudioClip FireProjectile;

	public GameObject projectilePrefab;
	protected string projectilePrefabName = "projectile";
	protected string projectilePrefabLocation = "Prefabs/Projectiles/";
	Player player;
	protected float fireRatePerSecond = 5f; //per second
	protected float fireCounter = 0;
	protected int projectileEnergyCost = 10;
	protected bool canShoot = true;

	protected Vector3 WeaponNozzleOffSet;

	protected void FireWeapon (){
		GameObject proj = Instantiate (projectilePrefab) as GameObject;
		proj.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		proj.GetComponent<Projectile> ().isEnergyType1 = player.isEnergyType1;
		proj.transform.position = transform.TransformPoint(WeaponNozzleOffSet);
		proj.transform.rotation = transform.rotation;

		audioSource.Play ();
	}

	protected float calculatedFireRate(){
		return 1f / fireRatePerSecond;
	}

	protected void Start(){
		player = GetComponent<Player> ();
	}

	protected void loadProjectilePrefab(){
		projectilePrefab = Resources.Load (projectilePrefabLocation+projectilePrefabName) as GameObject;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			if (canShootProjectile()) {
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

	bool canShootProjectile(){
		if (canShoot) {
			if (player.isEnergyType1 && (player.EnergyType1 - projectileEnergyCost) >= 0) {
				player.EnergyType1 -= projectileEnergyCost;
				return true;
			} else if (!player.isEnergyType1 && (player.EnergyType2 - projectileEnergyCost) >= 0) {
				player.EnergyType2 -= projectileEnergyCost;
					return true;
			} else {
					return false;
				}
		} else {
			return false;
		}

	}
}
