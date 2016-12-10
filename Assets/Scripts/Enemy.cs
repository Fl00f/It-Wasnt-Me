using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private int health = 100;
	public int Health{
		get{ 
			return health;
		}
	}

	// Use this for initialization
	void Start () {
		FloorSwitch.ChangeColors += ChangeMyColorMethod;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeMyColorMethod(Color changeToColor){}

	public void TakeDamage (int damageAmount){
		health -= damageAmount;
		if (health < 0) {
			health = 0;
			DestroySelf ();
		}
	}

	void DestroySelf(){
		FloorSwitch.ChangeColors -= ChangeMyColorMethod;

	}

	void OnCollisionEnter(Collision collision) {

	}
}
