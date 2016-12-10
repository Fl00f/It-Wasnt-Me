using UnityEngine;
using System.Collections;

public class FloorSwitch : MonoBehaviour {
	public delegate void ColorChangeMethod(Color changeToColor);
	public static ColorChangeMethod ChangeColors;

	public delegate void BehaviourChangeMethod(bool isGoodBehaviour);
	public static BehaviourChangeMethod ChangeBehaviours;

	public ColorsToChangeTo ColorOfSwitch;

	Renderer ren;

	// Use this for initialization
	void Start () {
		ren = GetComponent<Renderer> ();

		switch (ColorOfSwitch) {
			case ColorsToChangeTo.Blue:
				ren.material.color = Color.blue;
			break;
		case ColorsToChangeTo.Red:
				ren.material.color = Color.red;
			break;
		case ColorsToChangeTo.Green:
				ren.material.color = Color.green;
			break;
		case ColorsToChangeTo.Magenta:
				ren.material.color = Color.magenta;
			break;
			default:
			break;
		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SwitchColors(){
	}


	void OnCollisionEnter(Collision collision) {

	}

	public enum ColorsToChangeTo{
		Blue,
		Red,
		Green,
		Magenta
	}
}
