using UnityEngine;
using System.Collections;

public class MoveAlongRail : MonoBehaviour {

	public Vector3 StartPosition;
	public Vector3 StopPosition;


	public float speed = 1.0F;
	private float startTime = 0;
	public float JourneyLengthInSeconds = 5f; // time in seconds to travel length of rail
	bool flipTime = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float fracJourney = startTime / JourneyLengthInSeconds;
		startTime += Time.deltaTime;
		if (flipTime) {
			fracJourney = 1f - fracJourney;
		}
			
		transform.position = Vector3.Lerp(StartPosition, StopPosition, fracJourney);

		if (startTime > JourneyLengthInSeconds) {
			startTime = 0;
			flipTime = !flipTime;
		}
	}
}
