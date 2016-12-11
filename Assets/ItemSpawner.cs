using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : Spawner {

	public List<Transform> ItemSpawnLocations;
	public GameObject[] ItemPrefabsToSpawn;
	// Use this for initialization
	void Start () {
		spawnLocations = ItemSpawnLocations;
		prefabsToSpawn = ItemPrefabsToSpawn;
		foreach (var loc in GetComponentInChildren<Transform>()) {
			if (loc != transform) {
				spawnLocations.Add ((Transform)loc);
			}
		}
	}
}
