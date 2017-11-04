using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {
	private Transform target;
	public Rigidbody2D rb;
	public float lungeDelay;
	private float speedMod = 1;

	GameObject lastDamager;

	public override void Start(){
		rb = GetComponent<Rigidbody2D> ();
		target = GameObject.FindWithTag ("Player").transform;
		base.Start ();
	}

	public override void Damage(GameObject damager = (null), float amt = (1.0F), bool tickOnce = (false)){
		if (damager != lastDamager) {
			Vector3 screenSpacePosition = Camera.main.WorldToScreenPoint (transform.position);
			GameObject dmgInfoInst = Instantiate (textBoxObject, screenSpacePosition, Quaternion.identity) as GameObject;
			dmgInfoInst.transform.SetParent(GameObject.Find("Canvas").transform);
			dmgInfoInst.GetComponent<DamageInformation> ().damage = amt;

			Vector3 forceDirection = transform.position - target.position;
			GameObject.FindWithTag ("whiteScreenEffect").GetComponent<ScreenEffect> ().Begin ();
			if (damager.GetComponent<Bullet> () == null) {
				if (damager.GetComponent<SwingComponent> () != null) {
					rb.AddForce (forceDirection.normalized * damager.GetComponent<SwingComponent> ().swingForce);
					stunDuration = damager.GetComponent<SwingComponent> ().stunDuration;
					base.Damage (damager, amt);
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

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject != gameObject) {
			if (other.GetComponent<SwingComponent> () != null) {
				SwingComponent swingComponent = other.GetComponent<SwingComponent> ();
				float dmg = swingComponent.damage;
				Damage (other.gameObject, swingComponent.GetDamageOutput(), true);
			}
		}
	}

	void FixedUpdate(){
		if (stunDuration > 0) {
			GetComponent<BoxCollider2D> ().enabled = false;
		}
		if (target != null) {
			GetComponent<BoxCollider2D> ().enabled = true;
			float dist = (target.position - transform.position).magnitude;
			if (dist < 15) {
				if (dist > 2) {
					speed.SetMod(1);
					transform.position += (target.position - transform.position).normalized * speed.GetTotal() * Time.deltaTime;
				} else {
					speed.SetMod(2);
					lungeDelay -= Time.deltaTime;
					if (lungeDelay <= 0) {
						// Lunge towards player
						if (speedMod > 1)
							speedMod -= Time.deltaTime;
						if (stunDuration <= 0) {
							transform.position += (target.position - transform.position).normalized * Time.deltaTime * speed.GetTotal() * speedMod;
						}
					}
				}
			}
		}
	}
}
