using UnityEngine;

public class HealthPack : DroppedItem
{

	public Sprite healthPack;
	public Sprite bombPack;

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
		
		if (!isCurrentlyGoodBehaviour) {
			interactableWithplayer = false;
			foreach (var enemys in FindObjectsOfType<EnemyBase>()) {
				if (Vector3.Distance (transform.position, enemys.transform.position) < 2) {
					enemys.TakeDamage (100);
				}
			}
		} else {
			interactableWithplayer = true;
		}
	}

	public override void changeBehaviour (bool isGoodBehaviour)
	{
		this.isCurrentlyGoodBehaviour = isGoodBehaviour;
		//Visual changes are done here
		GetComponent<SpriteRenderer> ().sprite = isGoodBehaviour ? healthPack : bombPack;
		interactableWithplayer = !interactableWithplayer;
//		GetComponent<Renderer> ().material.color = Color.red; //For testing purposes
	}

	#endregion






}
