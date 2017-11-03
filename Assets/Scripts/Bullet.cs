using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public Vector3 direction;
	public float speed;

	float lifetime = 10;

	public bool destroyOnImpact;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other){
		if (destroyOnImpact) {
			
		}
	}

	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			Destroy (gameObject);
		}

		transform.position += direction * Time.deltaTime * speed;
	}
}
