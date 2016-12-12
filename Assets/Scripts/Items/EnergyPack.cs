using UnityEngine;
using System.Collections;

public class EnergyPack : DroppedItem {

	void Start(){
		base.Start ();
		isEnergyType1 = Random.Range (0, 2) == 0 ? true : false;
		changeColor (isEnergyType1);
	}

	public override void playerItemAction (Player player)
	{
		if (isCurrentlyGoodBehaviour) {
			player.AddEnergy (30, isEnergyType1);
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
		}
	}

	public override void changeBehaviour (bool isGoodBehaviour)
	{
		this.isCurrentlyGoodBehaviour = isGoodBehaviour;
		//Visual changes are done here


		//		GetComponent<Renderer> ().material.color = Color.red; //For testing purposes
	}
}
