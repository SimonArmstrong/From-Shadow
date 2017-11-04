using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyOld : Entity {
	public Transform target;
	public NavMeshAgent agent;
	public float move_speed;
	float speedMod;
	private GameObject lastDamager;

	GameObject player;

	private Rigidbody2D rb;

	public float lungeDelay = 1.0f;
	// Use this for initialization
	public override void Start () {
		base.Start ();
		if(GameObject.FindWithTag ("Player") != null)
		target = GameObject.FindWithTag ("Player").transform;
		speedMod = 1;
		rb = GetComponent<Rigidbody2D> ();
	}

	public override void Damage(GameObject damager, float amt = (1.0F), bool tickOnce = (false)){
		if (damager != lastDamager) {
			base.Damage (damager, amt, tickOnce);
			Vector3 forceDirection = transform.position - target.position;
			GameObject.FindWithTag ("whiteScreenEffect").GetComponent<ScreenEffect> ().Begin ();
			if (damager.GetComponent<Bullet> () == null) {
				if (damager.GetComponent<SwingComponent> () != null) {
					rb.AddForce (forceDirection.normalized * damager.GetComponent<SwingComponent> ().swingForce);
					stunDuration = damager.GetComponent<SwingComponent> ().stunDuration;
				}
		
			}
			if (rb.velocity.magnitude > 500) {
				float diff = rb.velocity.magnitude - 500;
				rb.AddForce (-rb.velocity * diff);
			}
			lastDamager = damager;
			GetComponent<AudioSource> ().Play ();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (stunDuration > 0) {
			GetComponent<BoxCollider2D> ().enabled = false;
		}
		if (target != null) {
				GetComponent<BoxCollider2D> ().enabled = true;
				float dist = (target.position - transform.position).magnitude;
				if (dist < 15) {
					if (dist > 2) {
						speedMod = 1;
					transform.position += (target.position - transform.position).normalized * speed.GetTotal() * Time.deltaTime;
					} else {
						speedMod = 2;
						lungeDelay -= Time.deltaTime;
						if (lungeDelay <= 0) {
							// Lunge towards player
							if (speedMod > 1)
								speedMod -= Time.deltaTime;
						if (stunDuration <= 0) {
							transform.position += (target.position - transform.position).normalized * move_speed * Time.deltaTime * speedMod;
						}
					}
				}
			}
		}
	}
}
