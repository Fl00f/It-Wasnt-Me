using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	static int timePerRound = 30;
	public static int CurrentTimeLeftInRound = 30;
	Text timerText;

	string timerString = "Time: ";
	// Use this for initialization
	void Start () {
		CurrentTimeLeftInRound = timePerRound;
		timerText = GetComponent<Text> ();
		InvokeRepeating ("DeductTimeFromTimers", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		timerText.text = timerString + CurrentTimeLeftInRound.ToString ();
	}

	void DeductTimeFromTimers(){
		if (CurrentTimeLeftInRound - 1 < 0) {
			CurrentTimeLeftInRound = 0;
		} else {
			CurrentTimeLeftInRound -= 1;
		}
	}

	public static void ResetTimer(){

		CurrentTimeLeftInRound = timePerRound;
	}
}
