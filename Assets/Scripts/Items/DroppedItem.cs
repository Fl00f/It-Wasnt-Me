using UnityEngine;
using System.Collections;

public abstract class DroppedItem : MonoBehaviour {
	protected bool isCurrentlyGoodBehaviour;
	// Use this for initialization
	protected void Start () {
		FloorSwitch.ChangeBehaviours += changeBehaviour;
	}

	void OnDestroy(){
		FloorSwitch.ChangeBehaviours -= changeBehaviour;
	}

	/// <summary>
	/// When behaviours are changed assign isCurrentlyGoodBehaviour and do any visual 
	/// changes for the GameObject here.
	/// </summary>
	/// <param name="isGoodBehaviour">If set to <c>true</c> is good behaviour.</param>
	public abstract void changeBehaviour(bool isGoodBehaviour);



	void OnTriggerEnter(Collider col) {
		if (col.gameObject.GetComponent<Player>()) {
			playerItemAction (col.gameObject.GetComponent<Player>());
			Destroy (gameObject);
		} else if (col.gameObject.GetComponent<Enemy>()) {
			enemyItemAction (col.gameObject.GetComponent<Enemy>());
			Destroy (gameObject);
			}
	}

	public abstract void playerItemAction (Player player);

	public abstract void enemyItemAction (Enemy enemy);
}
