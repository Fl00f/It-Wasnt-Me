using UnityEngine;
using System.Collections;

public class FloorSwitch : MonoBehaviour
{
	public delegate void ColorChangeMethod (Color changeToColor);

	public static ColorChangeMethod ChangeColors;

	public delegate void BehaviourChangeMethod (bool isGoodBehaviour);

	public static BehaviourChangeMethod ChangeBehaviours;

	public ColorsToChangeTo ColorOfSwitch;

	Renderer ren;

	protected SpriteRenderer colorChangeLayer;


	TimeCaptureTest timeCap;
	// Use this for initialization
	void Start ()
	{
		timeCap = FindObjectOfType<TimeCaptureTest> ();

		ren = GetComponent<Renderer> ();

		switch (ColorOfSwitch) {
			case ColorsToChangeTo.Blue:
				changeToCustomColor (Color.blue);
			break;
			case ColorsToChangeTo.Red:
				changeToCustomColor (Color.red);

			break;
			case ColorsToChangeTo.Green:
				changeToCustomColor (Color.green);

			break;
			case ColorsToChangeTo.Magenta:
				changeToCustomColor (Color.magenta);
			break;
			default:
			break;
		}
	}
	
	void changeToCustomColor(Color color){
		foreach (var item in GetComponentsInChildren<SpriteRenderer>()) {
			if (item.gameObject.name == "ChangeColorLayer") {
				item.color = color;
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Player> ()) {
			switch (ColorOfSwitch) {
				case ColorsToChangeTo.Blue:
					if (ChangeColors != null) {
						ChangeColors.Invoke (Color.blue);
					}
					timeCap.AddChangeColorTriggerData (true);

				break;
				case ColorsToChangeTo.Red:
					if (ChangeColors != null) {
						ChangeColors.Invoke (Color.red);

					}
					timeCap.AddChangeColorTriggerData (false);

				break;
				case ColorsToChangeTo.Green:
					if (ChangeBehaviours != null) {
						ChangeBehaviours.Invoke (true);
					}
					timeCap.AddChangeBehaviourTriggerData (true);

				break;
				case ColorsToChangeTo.Magenta:
					if (ChangeBehaviours != null) {
						ChangeBehaviours.Invoke (false);
					}
					timeCap.AddChangeBehaviourTriggerData (false);

				break;
				default:
				break;
			}
		}
	}

	public enum ColorsToChangeTo
	{
		Blue,
		Red,
		Green,
		Magenta
	}
}
