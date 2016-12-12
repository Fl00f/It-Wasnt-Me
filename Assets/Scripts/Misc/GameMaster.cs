using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public AudioSource aud;
	public AudioClip clip;

	int currentRound = 1;

	float dificultyMultiplier = 1f;
	float spawnEnemyTimer = 1f; //spawn every n seconds
	float spawnItemsTimer = 4f; //spawn every n seconds

	Timer timer;

	TimeCaptureTest[] timerCaps;

	EnemySpawner enemySpawner;
	public static List<EnemyBase> enemies = new List<EnemyBase>();

	ItemSpawner itemSpawner;
	public static List<GameObject> items = new List<GameObject>();

	public GameObject playerGO;
	public GameObject MainScreen;
	public GameObject WinScreen;
	public GameObject GameOverScreen;

	// Use this for initialization
	void Start () {
		timer = FindObjectOfType<Timer> ();
		timerCaps = FindObjectsOfType<TimeCaptureTest> ();

		enemySpawner = FindObjectOfType<EnemySpawner> ();
		itemSpawner = FindObjectOfType<ItemSpawner> ();



		foreach (var item in timerCaps) {
			item.enabled = false;
		}

		playerGO = FindObjectOfType<Player> ().gameObject;
		playerGO.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!aud.isPlaying) {
			aud.Play ();
		}
		#region ForTesting
		if (Input.GetKeyDown(KeyCode.C)) {
			StopRound ();
		}

		if (Input.GetKeyDown(KeyCode.V)) {
			StartRound (false);
		}
		#endregion
	

		if (Timer.CurrentTimeLeftInRound <= 0) {
			StopRound ();
			StartRound (true);
		}

	}

	public void StartGame(){
		playerGO.SetActive (true);
		MainScreen.SetActive (false);
		StartRound (false);
	}

	public void StartRound(bool isNewRound){

		if (isNewRound) {
			currentRound++;
			levelUp ();
		}

		Timer.ResetTimer();

		foreach (var item in timerCaps) {
			item.enabled = true;

			if (!item.GetComponent<Player>() && currentRound > 1) {
				GameObject spiralCenter = FindObjectOfType<SpinObject> ().gameObject; // short on time
				spiralCenter.SetActive (false);
				item.StartPlayBack (true);
			} else if(item.GetComponent<Player>()){
				FindObjectOfType<SpinObject> ().gameObject.SetActive (true);
				item.StartRecordingData(true);
			}

		}

		setSpawnTimers ();
		InvokeRepeating ("spawnEnemies", 0, spawnEnemyTimer);
		InvokeRepeating ("spawnItems", spawnItemsTimer, spawnItemsTimer);
	}

	public void StopRound(){

		GameObject temp = GameObject.Find ("MiniPlayer").gameObject;
		temp.GetComponent<TimeCaptureTest> ().StartPlayBack (false);

		GameObject temp2 = GameObject.Find ("Player").gameObject;
		temp2.GetComponent<TimeCaptureTest> ().FullStop ();


		foreach (var item in timerCaps) {
			item.enabled = false;
		}

		setSpawnTimers ();
		if (IsInvoking("spawnEnemies")) {
			CancelInvoke ();
		}

		if (enemies.Count != 0) {

			foreach (var item in FindObjectsOfType<EnemyBase>()) {
				item.DestroySelf ();
			}

			foreach (var item in FindObjectsOfType<DroppedItem>()) {
				item.DestroySelf ();
			}

			items.Clear ();
			enemies.Clear ();
		}

	}

	public void BackToMain(){
		MainScreen.SetActive (true);
		WinScreen.SetActive (false);
		GameOverScreen.SetActive (false);

	}

	public void GameOver(){
		GameOverScreen.SetActive (true);
		reset ();
	}

	public void Win(){
		WinScreen.SetActive (true);
	}

	void setSpawnTimers(){
		spawnEnemyTimer = spawnEnemyTimer * dificultyMultiplier;
		spawnItemsTimer = spawnItemsTimer * dificultyMultiplier;
	}

	void levelUp(){
		dificultyMultiplier *= 1.2f;
		setSpawnTimers ();
	}

	void reset(){

	}


	void spawnEnemies(){
		enemies.Add (enemySpawner.SpawnEnemy ());
	}

	void spawnItems(){
		items.Add (itemSpawner.SpawnObject ());
	}
}
