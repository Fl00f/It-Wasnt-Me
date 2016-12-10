using UnityEngine;
using System.Collections;

public abstract class WeaponControls : MonoBehaviour {

	protected float fireRate = 2; //per second

	public abstract void FireWeapon ();

}
