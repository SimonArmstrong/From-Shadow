using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float speed = 2;

	public TinkerAnimator animator;
	public Transform cursorPosition;
	public Rigidbody2D rb;
	public GameObject[] swingObject;

	// To deprecate
	public bool swingStep;
	public float swingForce;
	public float maxVelocity;
	float swingSpeed;
	float finalSpeed;

	public float dodgeCooldown;

	GameObject bullet;

	void Start(){
		animator = GetComponent<TinkerAnimator> ();
		rb = GetComponent<Rigidbody2D> ();

		bullet = Resources.Load ("Prefabs/Bullet") as GameObject;

		// To deprecate
		swingSpeed = swingForce;
		finalSpeed = speed;
	}

	Vector3 ZeroZ(Vector3 inVec){
		return new Vector3 (inVec.x, inVec.y, 0);
	}

	Vector3 Burst(Vector3 direction){
		Vector3 result = new Vector3();

		return result;
	}

	GameObject go;
	int comboCount = 0;
	float t = 0.1f;
	float comboDeadTimer = 0.5f;
	Vector3 movement;
	void Update(){
		if (Input.GetMouseButtonDown (0) && go == null) {
			Vector3 dir = (cursorPosition.position - transform.position).normalized;

			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			Quaternion rot = Quaternion.AngleAxis (angle - 90, Vector3.forward);

			go = Instantiate (swingObject [comboCount], ZeroZ ((transform.position + dir)), rot);
			swingForce = go.GetComponent<SwingComponent> ().swingStep;		

			//go.transform.parent = transform;
			rb.AddForce (ZeroZ ((cursorPosition.position - transform.position).normalized) * swingForce);

			//transform.position = Vector3.Lerp(transform.position, ZeroZ(transform.position - (transform.position + dir)).normalized, Time.deltaTime * (t * 30));
			//go.transform.rotation = Quaternion.AngleAxis (Vector3.Angle(cursorPosition.position, transform.position), Vector3.forward);
			t = 0.1f;
			speed = 0;
			comboCount++;
			comboDeadTimer = 0.5f;
			if (comboCount > swingObject.Length - 1) {
				comboCount = 0;
			}
		} else {
			speed = finalSpeed;
		}
		if (Input.GetMouseButtonDown (1)) {
			Vector3 dir = (cursorPosition.position - transform.position).normalized;

			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			Quaternion rot = Quaternion.AngleAxis (angle - 90, Vector3.forward);

			GameObject b = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
			b.GetComponent<Bullet> ().direction = dir;
		}

		if (Input.GetButtonDown ("Jump")) {
			rb.AddForce (ZeroZ (movement.normalized) * 900);
		}
		comboDeadTimer -= Time.deltaTime;
		if (comboCount > 0) {
			if (comboDeadTimer <= 0) {
				comboCount = 0;
				comboDeadTimer = 0.5f;
			}
		}

		float xMov = Input.GetAxisRaw ("Horizontal");
		float yMov = Input.GetAxisRaw ("Vertical");
		movement = new Vector3 (xMov, yMov, 0);
		movement.Normalize ();

		if (xMov > 0)
			animator.currentAnimation = 2;
		if (yMov > 0)
			animator.currentAnimation = 3;
		if (xMov < 0)
			animator.currentAnimation = 1;
		if (yMov < 0)
			animator.currentAnimation = 0;

	}
	void LateUpdate(){
		transform.position += movement * Time.deltaTime * finalSpeed;
	}
}
