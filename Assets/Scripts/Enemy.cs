using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	[SerializeField]
	private int health = 100;
	public int Health{
		get{ 
			return health;
		}
	}

	[SerializeField]
	private bool isEnergyType1 = false;

	// Use this for initialization
	void Start () {
		FloorSwitch.ChangeColors += ChangeEnemyColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeEnemyColor(Color changeToColor){
		GetComponent<Renderer> ().material.color = changeToColor;
		isEnergyType1 = changeToColor == Color.blue ? true : false;
	}

	public void TakeDamage (int damageAmount, bool isProjEnergyType1){
		if (isEnergyType1 == isProjEnergyType1) {
			health -= damageAmount;
		}

		if (health < 0) {
			health = 0;
			DestroySelf ();
		}
	}

	void DestroySelf(){
		FloorSwitch.ChangeColors -= ChangeEnemyColor;
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision collision) {

	}
}
