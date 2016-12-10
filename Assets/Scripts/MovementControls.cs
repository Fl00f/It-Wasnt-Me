using UnityEngine;
using System.Collections;

public class MovementControls : MonoBehaviour {
	float movementSpeed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		#region Controls

		//Left
		if (Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * Time.deltaTime * movementSpeed;
		}
		//Right
		if (Input.GetKey(KeyCode.D)) {

			transform.position += Vector3.right * Time.deltaTime * movementSpeed;
		}
		//Forward
		if (Input.GetKey(KeyCode.W)) {
			transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
		}
		//Back
		if (Input.GetKey(KeyCode.S)) {

			transform.position += Vector3.back * Time.deltaTime * movementSpeed;
		}
			
		#endregion

		lookAtMousePosition ();
	}

	RaycastHit hit;
	void lookAtMousePosition(){
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			Vector3 temp = hit.point - transform.position;
			temp.y = 0;
			transform.rotation = Quaternion.LookRotation(temp);
		}
	}
}
