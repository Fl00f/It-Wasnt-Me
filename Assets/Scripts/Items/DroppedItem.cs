using UnityEngine;
using System.Collections;

public abstract class DroppedItem : MonoBehaviour {
	public bool isCurrentlyGoodBehaviour;
	protected SpriteRenderer colorChangeLayer;
	protected bool isEnergyType1 = true;

	// Use this for initialization
	protected void Start () {
		FloorSwitch.ChangeBehaviours += changeBehaviour;
		setSpriteRenderer ();
	}

	/// <summary>
	/// When behaviours are changed assign isCurrentlyGoodBehaviour and do any visual 
	/// changes for the GameObject here.
	/// </summary>
	/// <param name="isGoodBehaviour">If set to <c>true</c> is good behaviour.</param>
	public abstract void changeBehaviour(bool isGoodBehaviour);

	public void DestroySelf(){
		FloorSwitch.ChangeBehaviours -= changeBehaviour;
		GameMaster.items.Remove (gameObject);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Player>()) {
			playerItemAction (col.gameObject.GetComponent<Player>());
			Destroy (gameObject);
		} else if (col.gameObject.GetComponent<EnemyBase>()) {
			enemyItemAction (col.gameObject.GetComponent<EnemyBase>());
			Destroy (gameObject);
			}
	}

	protected void setSpriteRenderer(){
		foreach (var item in GetComponentsInChildren<SpriteRenderer>()) {
			if (item.gameObject.name == "ChangeColorLayer") {
				colorChangeLayer = item;
			}
		}
	}
		

	protected void changeColor(bool isEnergyType1){
//		if (colorChangeLayer == null) {
//			setSpriteRenderer ();
//			colorChangeLayer.color = isEnergyType1 ? Color.blue : Color.red;
//
//		} else {
//			colorChangeLayer.color = isEnergyType1 ? Color.blue : Color.red;
//
//		}



		if (GetComponent<Renderer>()) {
			GetComponent<Renderer>().material.color = isEnergyType1 ? Color.blue : Color.red;
		}
	}

	public abstract void playerItemAction (Player player);

	public abstract void enemyItemAction (EnemyBase enemy);
}
