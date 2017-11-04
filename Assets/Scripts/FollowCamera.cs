using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	public Transform[] toFollow;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {

		Vector3 desiredPosition = new Vector3(0, 0, -10);

		for (int i = 0; i < toFollow.Length; i++) {
			if (toFollow [i] != null) {
				desiredPosition.x += toFollow [i].position.x;
				desiredPosition.y += toFollow [i].position.y;
			}
		}

		desiredPosition = desiredPosition / toFollow.Length;
		desiredPosition.z = -10;

		transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);
	}
}