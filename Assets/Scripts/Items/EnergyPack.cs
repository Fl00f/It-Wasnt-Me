using UnityEngine;
using System.Collections;

public class EnergyPack : DroppedItem
{

	void Start ()
	{
		base.Start ();
		isEnergyType1 = Random.Range (0, 2) == 0 ? true : false;
//		changeToCustomColor (new Color(0,149,255), new Color(255, 90,120));
		changeToCustomColor (new Color (0, 149, 255), Color.red);

	}

	public override void playerItemAction (Player player)
	{
		if (isCurrentlyGoodBehaviour) {
			player.AddEnergy (30, isEnergyType1);
			audioSource.clip = PickedUpItemGood;
			audioSource.Play ();
		} else {
			//TODO
		}


	}

	public override void enemyItemAction (EnemyBase enemy)
	{
		if (isCurrentlyGoodBehaviour) {
			//TODO
		} else {
			//TODO
			audioSource.clip = PickedUpItemBad;
			audioSource.Play ();

		}


	}

	public override void changeBehaviour (bool isGoodBehaviour)
	{
		this.isCurrentlyGoodBehaviour = isGoodBehaviour;
		//Visual changes are done here


		//		GetComponent<Renderer> ().material.color = Color.red; //For testing purposes
	}
}
