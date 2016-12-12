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
			foreach (var item in GetComponentsInChildren<SpriteRenderer>()) {
				if (item.gameObject.name == "ChangeColorLayer") {
					sr = item;
				}
			}
			sr.color = value ? Color.blue : Color.red;
//			GetComponent<Renderer> ().material.color = value ? Color.blue : Color.red;
			isEnergyType1 = value;
		}
	}


	SpriteRenderer sr;

	void Start () {
		start ();
	}
	protected void start () {
		FloorSwitch.ChangeColors += ChangeEnemyColor;
	}

	protected void ChangeEnemyColor(Color changeToColor){
		isEnergyType1 = changeToColor == Color.blue ? true : false;
		sr.color = changeToColor;
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

	public void DestroySelf(){
		FloorSwitch.ChangeColors -= ChangeEnemyColor;
		GameMaster.enemies.Remove (this);
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision collision) {

	}
}
