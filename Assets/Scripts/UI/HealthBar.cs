using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	Player player;

	float initWidth;
	float initHeigth;

	RectTransform rectT;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
		rectT = GetComponent<RectTransform> ();
		initWidth = rectT.rect.width;
		initHeigth = rectT.rect.height;
	}

	// Update is called once per frame
	void Update () {
		resizeEnergyBar ();
	}

	void resizeEnergyBar(){
		float temp = ((float)player.Health / (float)player.MAX_HEALTH) * initWidth;
		rectT.sizeDelta = new Vector2 (temp, initHeigth);
	}
}
