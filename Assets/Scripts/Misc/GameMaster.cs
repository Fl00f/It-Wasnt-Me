using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	public AudioSource aud;
	public AudioClip MainMenuClip;
	public AudioClip GamePlayClip;
	public AudioClip GameOverClip;
	public AudioClip WinClip;

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

	public GameObject DialogBox;

	public AudioSource EnemyDieSource;


	GameObject spiralCenter;
	// Use this for initialization
	void Start () {
		timer = FindObjectOfType<Timer> ();
		timerCaps = FindObjectsOfType<TimeCaptureTest> ();

		enemySpawner = FindObjectOfType<EnemySpawner> ();
		itemSpawner = FindObjectOfType<ItemSpawner> ();

		spiralCenter = FindObjectOfType<SpinObject> ().gameObject;

		foreach (var item in timerCaps) {
			item.enabled = false;
		}

		playerGO = FindObjectOfType<Player> ().gameObject;
		playerGO.SetActive (false);

		MainScreen.SetActive (true);
		WinScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		aud.clip = MainMenuClip;
		aud.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!aud.isPlaying) {
			aud.Play ();
		}
		#region ForTesting
		if (Input.GetKeyDown(KeyCode.C)) {
			GameOver();	
		}

		if (Input.GetKeyDown(KeyCode.V)) {
			Win();
		}
		#endregion
	

		if (Timer.CurrentTimeLeftInRound <= 0) {
			StopRound ();
			StartRound (true);
		} else if(Timer.CurrentTimeLeftInRound <= 0 && currentRound == 5){
				Win ();
		} else if(playerGO.GetComponent<Player>().IsDead){
					GameOver ();
		}


	}

	public void StartGame(){
		playerGO.SetActive (true);
		MainScreen.SetActive (false);
		StopRound (); //cheating
		aud.clip = GamePlayClip;
		aud.Play ();
		aud.loop = true;
//		StartRound (false);
	}

	public void StartRound(bool isNewRound){
		if (isNewRound) {
			spiralCenter.SetActive (true);
			currentRound++;
			levelUp ();
		} else {
			spiralCenter.SetActive (false);
		}
		enemySpawner.CanSpawn = true;
		itemSpawner.CanSpawn = true;

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
		timer.StopTimer ();
		switch (currentRound) {
			case 1:
				showDialogBox (true, "Hay Marine! We just got sucked into a worm hole. Some weird stuff is happening all over the ship!" +
					"We can get to you right now so your stuck in that room. Kill any bad guys if they come your way." +
					"And remember...if anything goes wrong...IT'S YOUR FAULT...good luck...no pressure.");
			break;
			case 2:
				showDialogBox (true, "What in the world were those things?! Never mind that now. Have you figure out what those switched do in the room?" +
					"They may be the key to getting out alive! Good luck!");
			break;
			case 3:
				showDialogBox (true, "What!? There's two of you? How can this be? Wait. That looks like you from a few seconds ago. Past you looks up to no good.");
			break;
			case 4:
				showDialogBox (true, "Things seem to be going wrong! It's so far its your fault!");
			break;
			default:
			break;
		}

		enemySpawner.CanSpawn = false;
		itemSpawner.CanSpawn = false;

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

		if (currentRound == 5) {
			Win ();
		}
	}

	public void BackToMain(){
		MainScreen.SetActive (true);
		WinScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		aud.clip = MainMenuClip;
		aud.loop = true;

	}

	public void GameOver(){
		GameOverScreen.SetActive (true);
		aud.clip = GameOverClip;
		aud.loop = false;

		aud.Play ();
		reset ();
	}

	public void Win(){
		aud.clip = WinClip;
		aud.loop = false;
		aud.Play ();
		WinScreen.SetActive (true);
		reset ();
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
		currentRound = 1;
		itemSpawner.enabled = false;
		itemSpawner.CanSpawn = false;

		enemySpawner.CanSpawn = false;
		enemySpawner.enabled = false;

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


	void spawnEnemies(){
		if (enemySpawner.CanSpawn) {
			enemies.Add (enemySpawner.SpawnEnemy ());

		}
	}

	void spawnItems(){
		if (itemSpawner.CanSpawn) {
			items.Add (itemSpawner.SpawnObject ());
		}
	}


	void showDialogBox(bool show, string text){

		DialogBox.transform.FindChild ("Text").GetComponent<Text> ().text = text;
		DialogBox.SetActive (show);
	}

	public void diagButton(){
		if (currentRound == 1) {
			StartRound (true);
		} else if(currentRound == 5){

			} else {
			StartRound (false);

			}
		DialogBox.SetActive (false);
		timer.StartTimer ();

	}
}
