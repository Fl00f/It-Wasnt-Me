using UnityEngine;
using System.Collections;

public class EnergyBar : MonoBehaviour {
	Player player;
	public bool IsEnergyType1 = false;

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

	Rect temp;
	void resizeEnergyBar(){
		float temp = IsEnergyType1 ? ((float)player.EnergyType1 / (float)player.MAX_ENERGY) * initWidth : ((float)player.EnergyType2 / (float)player.MAX_ENERGY) * initWidth;
		rectT.sizeDelta = new Vector2 (temp, initHeigth);

	}
}
