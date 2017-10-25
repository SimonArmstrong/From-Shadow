using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static int score;
	public float spawnRate = 5.0f;
	float spawnTimer;

	public GameObject enemy;

	// Use this for initialization
	void Start () {
		spawnTimer = 0;
		enemy = Resources.Load ("Prefabs/Enemy") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0) {
			Vector3 randomPosition = new Vector3 (Random.Range (-2.0f, 2.0f), Random.Range (-2.0f, 2.0f), 0);
			Vector3 spawnRandomPosition = randomPosition.normalized * 10;

			Instantiate (enemy, spawnRandomPosition, Quaternion.identity);
			spawnTimer = spawnRate;
		}
	}
}
