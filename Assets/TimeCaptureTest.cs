using UnityEngine;
using System.Collections.Generic;

public class TimeCaptureTest : MonoBehaviour
{

	public static List<Vector3> RecordedMovData;
	public static List<Quaternion> RecordedRotData;
	int count = 0;
	float framesPerSecound = 30f;
	float timer = 0;


	public static bool Recording = false;
	public static bool PlayingBack = false;

	bool isMinion = true;
	// Use this for initialization
	void Start ()
	{
		if (GetComponent<Player>()) {
			RecordedMovData = new List<Vector3> ();
			RecordedRotData = new List<Quaternion> ();
			isMinion = false;
		}

	}
	
	// Update is called once per frame
	void Update ()
	{

		if (isMinion && !playBackData ()) {
			return;
		}

		if (!isMinion && !recordData ()) {
			return;
		}


		if (Input.GetKeyDown(KeyCode.I)) {
			Recording = true;
			PlayingBack = false;
		}

		if (Input.GetKeyDown(KeyCode.O)) {
			Recording = false;
			PlayingBack = true;
		}

		count = 0;

	}


	bool recordData ()
	{
		if (Recording) {
			timer += Time.deltaTime;

			if (timer > 1f / framesPerSecound) {
				RecordedMovData.Add (transform.position);
				RecordedRotData.Add (transform.rotation);
				count++;
				timer = 0;
			}
			return false;
		} else {
			return true;
		}
	}

	bool playBackData ()
	{

		if (PlayingBack) {
			timer += Time.deltaTime;
			if (count != 0 && count < RecordedMovData.Count) {
				transform.position = Vector3.Lerp (RecordedMovData [count - 1], RecordedMovData [count], (1f / framesPerSecound) * timer) * .2f;
				transform.rotation = Quaternion.Lerp (RecordedRotData [count - 1], RecordedRotData [count], (1f / framesPerSecound) * timer);
			}

			if (timer > 1f / framesPerSecound) {
//				transform.position = testing [count];
				count++;
				timer = 0;
			}
			return false;
		} else {
			return true;
		}
	}
}
