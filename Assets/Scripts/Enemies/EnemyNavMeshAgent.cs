using UnityEngine;
using System.Collections;

public class EnemyNavMeshAgent : MonoBehaviour {

	Transform goal;
	NavMeshAgent nav;
	Player player;
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		player = FindObjectOfType<Player> ();
		goal = player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		nav.destination = goal.position;
	}
}
