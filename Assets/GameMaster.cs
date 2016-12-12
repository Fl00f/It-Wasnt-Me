using UnityEngine;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

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
	// Use this for initialization
	void Start () {
		timer = FindObjectOfType<Timer> ();
		timerCaps = FindObjectsOfType<TimeCaptureTest> ();

		enemySpawner = FindObjectOfType<EnemySpawner> ();
		itemSpawner = FindObjectOfType<ItemSpawner> ();

		foreach (var item in timerCaps) {
			item.enabled = false;
		}


		StartGame ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		StartRound (false);
	}

	public void StartRound(bool isNewRound){
		if (isNewRound) {
			currentRound++;
		}

		Timer.ResetTimer();

		foreach (var item in timerCaps) {
			item.enabled = true;

			if (!item.GetComponent<Player>() && currentRound > 1) {
				item.StartPlayBack (true);
			} else if(item.GetComponent<Player>()){
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
