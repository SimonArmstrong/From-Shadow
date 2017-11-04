using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {
	private float iFrameTimer = 0;
	public Weapon weapon;

	public Transform cursorPosition;

	private TinkerAnimator animator;
	private Rigidbody2D rb;

	// To deprecate
	public float maxVelocity;
	float swingSpeed;
	float finalSpeed;
	float moveSpeed;
	public float dodgeCooldown;

	GameObject bullet;

	public void EquipWeapon(Weapon wep){
		weapon = wep;
	}

	public override void Damage (GameObject damager = (null), float amt = (1.0F), bool tickOnce = (false)) {
		if (iFrameTimer <= 0) {
			GameObject.FindWithTag ("redScreenEffect").GetComponent<ScreenEffect> ().Begin ();
			health.Add(-amt);
			iFrameTimer = 1;
		}
		if (health.GetTotal() <= 0) {
			Die ();
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<EnemyDamageComponent> () != null) {
			Damage (other.GetComponent<EnemyDamageComponent>().gameObject, other.GetComponent<EnemyDamageComponent>().dmg);
		}
	}


	public override void Start(){
		base.Start ();
		moveSpeed = speed.GetTotal();
		animator = GetComponent<TinkerAnimator> ();
		rb = GetComponent<Rigidbody2D> ();

		bullet = Resources.Load ("Prefabs/Bullet") as GameObject;

		// To deprecate
		swingSpeed = weapon.stepForce;
		finalSpeed = moveSpeed;
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
		if(iFrameTimer > 0)
			iFrameTimer -= Time.deltaTime;
		if (Input.GetButtonDown ("Fire1") && go == null) {
			Vector3 dir = (cursorPosition.position - transform.position).normalized;

			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			Quaternion rot = Quaternion.AngleAxis (angle - 90, Vector3.forward);

			go = Instantiate (weapon.comboObjects [comboCount], ZeroZ ((transform.position + dir)), rot);
			go.GetComponent<SwingComponent> ().LoadWeaponStats(weapon);

			//go.transform.parent = transform;
			rb.AddForce (ZeroZ ((cursorPosition.position - transform.position).normalized) * go.GetComponent<SwingComponent> ().swingStep);

			//transform.position = Vector3.Lerp(transform.position, ZeroZ(transform.position - (transform.position + dir)).normalized, Time.deltaTime * (t * 30));
			//go.transform.rotation = Quaternion.AngleAxis (Vector3.Angle(cursorPosition.position, transform.position), Vector3.forward);
			t = 0.1f;
			moveSpeed = 0;
			comboCount++;
			comboDeadTimer = 0.5f;
			if (comboCount > weapon.comboObjects.Length - 1) {
				comboCount = 0;
			}
		} else {
			moveSpeed = finalSpeed;
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

		if (Input.GetButtonDown ("Cancel")) {
			Time.timeScale = 0;
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
