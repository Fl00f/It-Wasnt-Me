using UnityEngine;
using System.Collections;

public class DroppedItem : MonoBehaviour {
	protected bool isCurrentlyGoodBehaviour;
	// Use this for initialization
	protected void Start () {
		FloorSwitch.ChangeBehaviours += changeBehaviour;
		print ("Got it");
	}

	protected void changeBehaviour(bool isGoodBehaviour){
		this.isCurrentlyGoodBehaviour = isGoodBehaviour;
	}

	protected void OnDestroy(){
		FloorSwitch.ChangeBehaviours -= changeBehaviour;
	}

	protected void OnCollisionEnter(Collision collision) {

	}
}
