using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static int score;
	public static int wave = 1;
	public static int enemiesThisWave = 10;
	public static float enemyCount;
	public float spawnRate = 5.0f;
	float spawnTimer;
	public static GameObject healthDropItem;
	public GameObject enemy;
	public static int kills;

	public List<GameObject> waveLoot = new List<GameObject>();

	// Use this for initialization
	void Start () {
		spawnTimer = 0;
		enemy = Resources.Load ("Prefabs/Enemy/Wave " + wave + "/Enemy " + wave) as GameObject;
		GameManager.healthDropItem = Resources.Load ("Prefabs/HealthDrop") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
		if (GameManager.kills >= enemiesThisWave) {
			GameManager.wave++;
			enemy = Resources.Load ("Prefabs/Enemy/Wave " + wave + "/Enemy " + wave) as GameObject;
			GameManager.kills = 0;
			GameManager.enemyCount = 0;
			GameManager.enemiesThisWave = 1;

			Instantiate (waveLoot [Random.Range (0, waveLoot.Count)], Vector3.zero, Quaternion.identity);
		}
		if (GameManager.enemyCount < GameManager.enemiesThisWave) {
			spawnTimer -= Time.deltaTime;
			if (spawnTimer <= 0) {
				Vector3 randomPosition = new Vector3 (Random.Range (-2.0f, 2.0f), Random.Range (-2.0f, 2.0f), 0);
				Vector3 spawnRandomPosition = randomPosition.normalized * 10;

				Instantiate (enemy, spawnRandomPosition, Quaternion.identity);
				spawnTimer = spawnRate;
				GameManager.enemyCount++;
			}
		}
	}
}
