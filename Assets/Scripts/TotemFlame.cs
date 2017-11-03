using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemFlame : MonoBehaviour
{
    public GameObject fireball;

    public float spawnCooldown;
    float currentCooldown;

	void Start ()
    {
        currentCooldown = 0;
	}
	
	void Update ()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0)
        {
            currentCooldown = spawnCooldown;

            GameObject newFireball = Instantiate(fireball, transform.position, Quaternion.identity);
            newFireball.GetComponent<Velocity>().velocity = GetComponent<AttackObject>().direction + Random.insideUnitCircle * 0.5f;
            newFireball.GetComponent<Velocity>().velocity = newFireball.GetComponent<Velocity>().velocity.normalized * 5;
            newFireball.GetComponent<Damage>().damage = GetComponent<AttackObject>().damage;
            Debug.Log(newFireball.GetComponent<Damage>().damage);
        }
	}
}
