/*
 * Good to use as first template
 */ 
public class HealthPack : DroppedItem {
	#region implemented abstract members of DroppedItem

	public override void playerItemAction (Player player)
	{
		if (isCurrentlyGoodBehaviour) {
			player.AddHealth (100);
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
	#endregion






}
