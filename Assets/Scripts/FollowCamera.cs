using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

	public Transform toFollow;
	public float speed;

    public float mouseOffset;
    Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = Vector3.zero;
	}


	// Update is called once per frame
	void Update () {

		Vector3 desiredPosition = new Vector3(0, 0, -10);
        
			if (toFollow != null) {
				desiredPosition.x = toFollow.position.x;
				desiredPosition.y = toFollow.position.y;
			}
            
		desiredPosition.z = -10;

        transform.position -= offset;
        
		transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * speed);

        offset = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.width, 0) * mouseOffset;
        transform.position += offset;
    }
}