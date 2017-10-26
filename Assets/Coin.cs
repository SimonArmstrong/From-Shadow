using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private GameObject player;
	public int value;

	// Use this for initialization
	void Start () {
        player = Player.instance.gameObject;
		GetComponent<BoxCollider2D> ().enabled = false;

		Vector3 randomPosition = new Vector3 (Random.Range (-2.0f, 2.0f), Random.Range (-2.0f, 2.0f), 0);
		Vector3 spawnRandomPosition = randomPosition.normalized * 0.5f;

		GetComponent<Rigidbody2D> ().AddForce (spawnRandomPosition * 300);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.collider.GetComponent<Movement> () != null) {
			GetComponent<AudioSource> ().Play ();
			GameManager.score += value;
			Destroy (gameObject);
		}
	}
	float t = 1;
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime;
		if (t <= 0) {
			GetComponent<BoxCollider2D> ().enabled = true;
			float dist = (player.transform.position - transform.position).magnitude;
			if(dist < 2){
				transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * 5);
			}
		}
	}
}
