using UnityEngine;
using System.Collections;

public class MovementControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#region Controls

		//Left
		if (Input.GetKeyDown(KeyCode.A)) {
			
		}
		//Right
		if (Input.GetKeyDown(KeyCode.D)) {

		}
		//Up
		if (Input.GetKeyDown(KeyCode.W)) {

		}
		//Down
		if (Input.GetKeyDown(KeyCode.S)) {

		}
		//Fire
		if (Input.GetKeyDown(KeyCode.Space)) {

		}
			
		#endregion

		lookAtMousePosition ();
	}

	void lookAtMousePosition(){
	}
}
