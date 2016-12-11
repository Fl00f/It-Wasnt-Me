using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {
	[SerializeField]
	private int health = 100;
	public int Health{
		get{ 
			return health;
		}
	}

	protected int damageAmount;

	private bool isEnergyType1 = false;

	public bool IsEnergyType1 {
		get {
			return isEnergyType1;
		}
		set {
			GetComponent<Renderer> ().material.color = value ? Color.blue : Color.red;
			isEnergyType1 = value;
		}
	}

	void Start () {
		start ();
	}
	protected void start () {
		FloorSwitch.ChangeColors += ChangeEnemyColor;
	}

	protected void ChangeEnemyColor(Color changeToColor){
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

	protected void DestroySelf(){
		FloorSwitch.ChangeColors -= ChangeEnemyColor;
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision collision) {

	}
}
