using UnityEngine;
using System.Collections.Generic;

public class TimeCaptureTest : MonoBehaviour
{

	public static List<Vector3> RecordedMovData;
	public static List<Quaternion> RecordedRotData;

	public static List<Vector3> PlayBackMovData;
	public static List<Quaternion> PlayBackRotData;

	int playBackCounter = 0;
	int recordCounter = 0;
	float framesPerSecound = 30f;
	float recordingTimer = 0;

	float triggerTimer = 0;
	int triggerBehaviourDataCounter = 0;
	int triggerColorDataCounter = 0;

	public static List<float> RecChangeColorTime = new List<float> ();
	public static List<bool> RecChangeColorData = new List<bool> ();

	public static List<float> RecChangeBehaviourTime = new List<float> ();
	public static List<bool> RecChangeBehaviourData = new List<bool> ();

	public static List<float> PBChangeColorTime = new List<float> ();
	public static List<bool> PBChangeColorData = new List<bool> ();

	public static List<float> PBChangeBehaviourTime = new List<float> ();
	public static List<bool> PBChangeBehaviourData = new List<bool> ();

	public static bool Recording = false;
	public static bool PlayingBack = false;

	bool isMinion = true;
	// Use this for initialization
	void Start ()
	{
		if (GetComponent<Player> ()) {
			RecordedMovData = new List<Vector3> ();
			RecordedRotData = new List<Quaternion> ();
			isMinion = false;
		} else {
			PlayBackMovData = new List<Vector3> ();
			PlayBackRotData = new List<Quaternion> ();
		}

	}
	
	// Update is called once per frame
	void Update ()
	{

		print ("Rec: " + RecChangeColorData.Count);
		print ("PB: " + PBChangeColorData.Count);

		if (GetComponent<Player> ()) {
			if (Input.GetKeyDown (KeyCode.I)) {
				StartRecordingData (true);
			}

		} else {
			if (Input.GetKeyDown (KeyCode.O)) {
				StartPlayBack (true);
			}
		}

		triggerTimer += Time.deltaTime;

		if (isMinion && !playBackData ()) {
			return;
		}

		if (!isMinion && !recordData ()) {
			return;
		}


		playBackCounter = 0;
		recordCounter = 0;


	}

	#region Playing and Recording Mov and Rot Data


	public void FullStop(){
		Recording = false;
		PlayingBack = false;

		triggerTimer = 0;

		triggerBehaviourDataCounter = 0;
		triggerColorDataCounter = 0;
		playBackCounter = 0;

		SaveRecordedData ();
//		SaveSwitchData ();
	}

	public void StartRecordingData (bool shouldStart)
	{
		Recording = shouldStart;
		recordingTimer = 0;
		triggerTimer = 0;
//		PlayingBack = !shouldStart;

//		if (!shouldStart) { 
//			SaveRecordedData ();
//			resetRecordedData ();// should be ready for play back
//			ResetRecSwitchData ();
//		}
		print ("Recording: " + shouldStart);
	}

	public void StartPlayBack (bool shouldStart)
	{
//		Recording = !shouldStart;
		PlayingBack = shouldStart;
		triggerTimer = 0;

		triggerBehaviourDataCounter = 0;
		triggerColorDataCounter = 0;
		playBackCounter = 0;
//		if (!shouldStart) {
//			resetPlayBackData ();
//			ResetPBSwitchData ();
//		}

		print ("Playing Back: " + shouldStart);
	}

	void resetRecordedData ()
	{
		RecordedMovData = new List<Vector3> ();
		RecordedRotData = new List<Quaternion> ();
	}

	public void SaveRecordedData ()
	{
		PlayBackMovData = RecordedMovData;
		PlayBackRotData = RecordedRotData;

		SaveSwitchData ();

		//reset recorded data
		resetRecordedData ();
		ResetRecSwitchData ();
	}

	void resetPlayBackData ()
	{
		PlayBackMovData = new List<Vector3> ();
		PlayBackRotData = new List<Quaternion> ();
	}

	bool recordData ()
	{
		if (Recording) {
			recordingTimer += Time.deltaTime;

			if (recordingTimer > 1f / framesPerSecound) {
				RecordedMovData.Add (transform.position);
				RecordedRotData.Add (transform.rotation);
				recordCounter++;
				recordingTimer = 0;
			}
			return false;
		} else {
			return true;
		}
	}

	bool playBackData ()
	{
		if (PlayingBack) {
			recordingTimer += Time.deltaTime;
			if (playBackCounter != 0 && playBackCounter < PlayBackMovData.Count) {
				transform.position = Vector3.Lerp (PlayBackMovData [playBackCounter - 1], PlayBackMovData [playBackCounter], (1f / framesPerSecound) * recordingTimer) * .2f;
				transform.rotation = Quaternion.Lerp (PlayBackRotData [playBackCounter - 1], PlayBackRotData [playBackCounter], (1f / framesPerSecound) * recordingTimer);
			}

			if (recordingTimer > 1f / framesPerSecound) {
				playBackCounter++;
				recordingTimer = 0;
			}
				
				//trigger data
				if (PBChangeBehaviourTime.Count > 0 & triggerBehaviourDataCounter < PBChangeBehaviourTime.Count) {
					if (FloorSwitch.ChangeBehaviours != null && PBChangeBehaviourTime [triggerBehaviourDataCounter] < triggerTimer) {
						FloorSwitch.ChangeBehaviours.Invoke (PBChangeBehaviourData [triggerBehaviourDataCounter]);
						triggerBehaviourDataCounter++;
					}

				}

				if (PBChangeColorTime.Count > 0 & triggerColorDataCounter < PBChangeColorTime.Count ) {
					if (FloorSwitch.ChangeColors != null & PBChangeColorTime [triggerColorDataCounter] < triggerTimer) {
						if (PBChangeColorData [triggerColorDataCounter]) {
							FloorSwitch.ChangeColors.Invoke(Color.blue); //isEnergyType1 = true
						} else {
							FloorSwitch.ChangeColors.Invoke(Color.red); //isEnergyType1 = false

						}
						triggerColorDataCounter++;
					}

				}
			return false;
		} else {
			return true;
		}
	}

	#endregion

	#region Switch methods

	public void AddChangeColorTriggerData (bool isEnergyType1)
	{
		print ("added Change color");
		RecChangeColorData.Add (isEnergyType1);
		RecChangeColorTime.Add (triggerTimer);
	}

	public void AddChangeBehaviourTriggerData (bool isGoodBehaviour)
	{
		print ("added Change BH");

		RecChangeBehaviourData.Add (isGoodBehaviour);
		RecChangeBehaviourTime.Add (triggerTimer);
	}

	public void ResetRecSwitchData ()
	{
		RecChangeBehaviourData = new List<bool> ();
		RecChangeBehaviourTime = new List<float> ();
		RecChangeColorData = new List<bool> ();
		RecChangeColorTime = new List<float> ();
	}

	public void ResetPBSwitchData ()
	{
		PBChangeColorData.Clear ();
		PBChangeColorTime.Clear ();
		PBChangeBehaviourData.Clear ();
		PBChangeBehaviourTime.Clear ();
	}

	public void SaveSwitchData ()
	{
		PBChangeColorData = RecChangeColorData;
		PBChangeColorTime = RecChangeColorTime;
		PBChangeBehaviourData = RecChangeBehaviourData;
		PBChangeBehaviourTime = RecChangeBehaviourTime;
	}

	#endregion


}
