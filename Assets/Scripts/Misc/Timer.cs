using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	static int timePerRound = 60;
	public static int CurrentTimeLeftInRound = 30;
	Text timerText;

	string timerString = "Time: ";
	// Use this for initialization
	void Start () {
		CurrentTimeLeftInRound = timePerRound;
		timerText = GetComponent<Text> ();
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

	public void StopTimer(){

		CancelInvoke ();
	}

	public void StartTimer(){
		InvokeRepeating ("DeductTimeFromTimers", 1, 1);

	}

	public static void ResetTimer(){

		CurrentTimeLeftInRound = timePerRound;
	}
}
