using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FloorSwitch.ChangeColors += changeColor;
	}

	void changeColor(Color changeToColor){
	}

	void OnDestroy(){
		FloorSwitch.ChangeColors -= changeColor;
	}
}
