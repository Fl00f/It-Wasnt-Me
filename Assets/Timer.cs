using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	int timePerRound = 60;
	public static int CurrentTimeLeftInTimer = 0;
	Text timerText;

	string timerString = "Time: ";
	// Use this for initialization
	void Start () {
		CurrentTimeLeftInTimer = timePerRound;
		timerText = GetComponent<Text> ();
		InvokeRepeating ("DeductTimeFromTimers", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		timerText.text = timerString + CurrentTimeLeftInTimer.ToString ();
	}

	void DeductTimeFromTimers(){
		if (CurrentTimeLeftInTimer - 1 < 0) {
			CurrentTimeLeftInTimer = 0;
		} else {
			CurrentTimeLeftInTimer -= 1;
		}
	}
}
