using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[System.Serializable]
	public struct Health {
		private float cur;
		private float mod;
		public float max;

		public float GetCurrent(){
			return cur * mod;
		}

		public void SetCurrent(float amt){
			if (amt < max)
				cur = amt;
			else
				cur = max;
		}

		public void SetMod(float m){
			mod = m;
		}

		public void Init(float amt){
			max = amt;
			cur = amt;
			mod = 1;
		}
	}

	private float iFrameTimer = 0;
	public Health health;

	void Die(){
		Destroy (gameObject);
	}

	public void Damage (float dmg){
		if (iFrameTimer <= 0) {
			GameObject.FindWithTag ("redScreenEffect").GetComponent<ScreenEffect> ().Begin ();
			health.SetCurrent (health.GetCurrent () - dmg);
			iFrameTimer = 1;
		}
		if (health.GetCurrent () <= 0) {
			Die ();
		}
	}
		
	// Use this for initialization
	void Start () {
		health = new Health ();
		health.Init (3);
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.GetComponent<EnemyDamageComponent> () != null) {
			Damage (other.GetComponent<EnemyDamageComponent>().dmg);
		}
	}

	// Update is called once per frame
	void Update () {
		if(iFrameTimer > 0)
			iFrameTimer -= Time.deltaTime;
	}
}
