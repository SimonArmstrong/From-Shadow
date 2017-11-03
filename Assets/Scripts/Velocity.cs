using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    public Vector3 velocity;
	
	void Update ()
    {
        transform.position += velocity * Time.deltaTime;
	}
}
