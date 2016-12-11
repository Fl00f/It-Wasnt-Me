using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	protected List<Transform> spawnLocations;
	protected GameObject[] prefabsToSpawn;

	// Use this for initialization
	void Start () {
		spawnLocations = new List<Transform>();

		foreach (var loc in GetComponentInChildren<Transform>()) {
			if (loc != transform) {
				spawnLocations.Add ((Transform)loc);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SpawnObject ();
		}
	}

	protected GameObject getPrefab(){
		return prefabsToSpawn[Random.Range(0,prefabsToSpawn.Length)];
	}

	public void SpawnObject(){
		try {
			GameObject prefab = getPrefab();
			GameObject temp = Instantiate(prefab) as GameObject;
			if (temp.GetComponent<EnemyBase> ()) {
				temp.GetComponent<EnemyBase> ().IsEnergyType1 = getEnergyTypeForSpawn ();
			} else if (temp.GetComponent<DroppedItem> ()) {
				temp.GetComponent<DroppedItem> ().isCurrentlyGoodBehaviour = getEnergyTypeForSpawn ();
			}
			temp.transform.position = spawnLocations [Random.Range(0,spawnLocations.Count)].position;
		} catch (System.Exception ex) {
			print ("asda");
		}

	}

	protected bool getEnergyTypeForSpawn(){
		return Random.Range (0, 2) == 0;
	}
}
