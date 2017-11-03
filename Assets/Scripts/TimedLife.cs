using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{
    public float life;
	
	void Update ()
    {
        life -= Time.deltaTime;
        if (life <= 0)
            Destroy(gameObject);
	}
}
