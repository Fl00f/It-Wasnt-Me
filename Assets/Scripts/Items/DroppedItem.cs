using UnityEngine;
using System.Collections;

public abstract class DroppedItem : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioClip PickedUpItemGood;
	public AudioClip PickedUpItemBad;

	public bool isCurrentlyGoodBehaviour;
	protected SpriteRenderer colorChangeLayer;
	protected bool isEnergyType1 = true;
	protected bool interactableWithplayer = true;
	// Use this for initialization
	protected void Start ()
	{
		audioSource = GetComponent<AudioSource> ();

		FloorSwitch.ChangeBehaviours += changeBehaviour;
		setChangeColorLayer ();
	}

	/// <summary>
	/// When behaviours are changed assign isCurrentlyGoodBehaviour and do any visual 
	/// changes for the GameObject here.
	/// </summary>
	/// <param name="isGoodBehaviour">If set to <c>true</c> is good behaviour.</param>
	public abstract void changeBehaviour (bool isGoodBehaviour);

	public void DestroySelf ()
	{
		FloorSwitch.ChangeBehaviours -= changeBehaviour;
		GameMaster.items.Remove (gameObject);
		Destroy (gameObject);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Player> () && interactableWithplayer) {
			playerItemAction (col.gameObject.GetComponent<Player> ());
			DestroySelf ();
		} 

		if (col.gameObject.GetComponent<EnemyBase> () && !interactableWithplayer) {
			enemyItemAction (col.gameObject.GetComponent<EnemyBase> ());
			DestroySelf ();
		}
	}

	bool setChangeColorLayer ()
	{
		foreach (var item in GetComponentsInChildren<SpriteRenderer>()) {
			if (item.gameObject.name == "ChangeColorLayer") {
				colorChangeLayer = item;
				return true;
			}
		}

		return false;
	}

	void setSpriteRenderer ()
	{
		colorChangeLayer = GetComponent<SpriteRenderer> ();
	}

	protected void changeToCustomColor (Color ET1Color, Color ET2Color)
	{
		if (colorChangeLayer == null) {
			if (!setChangeColorLayer ()) {
				setSpriteRenderer ();
			}
		} 

		colorChangeLayer.color = isEnergyType1 ? ET1Color : ET2Color;
	}

	protected void changeColor (bool isEnergyType1)
	{
		if (colorChangeLayer == null) {
			if (!setChangeColorLayer ()) {
				setSpriteRenderer ();
			}
		} 
		colorChangeLayer.color = isEnergyType1 ? Color.blue : Color.red;

	}

	public abstract void playerItemAction (Player player);

	public abstract void enemyItemAction (EnemyBase enemy);
}
