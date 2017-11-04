using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Entity : TinkerObject {
	public int creatureIndex;
	public List<SpriteRenderer> otherEntitySprites = new List<SpriteRenderer>();
	[System.Serializable]
	public struct Stat{
		public Buff buff;
		private float permanentMod;
		private float cur, mod, total;
		public float max;
		public int level;

		public void Level(){
			level++;
			permanentMod += 0.1f;
		}

		public void Set(float amt){
			cur = amt;
			if (cur >= max)
				cur = max;
		}

		public void SetMod(float m){
			mod = m;
		}

		public void Add(float amt){
			cur += amt;
		}

		public float GetTotal(){
			if(buff != null)
				total = cur * (1 + (mod + permanentMod)) * buff.amount;
			else
				total = cur * (1 + (mod + permanentMod));
			
			return total;
		}

		public void Init(){			
			cur = max;
		}
	}
	public Stat health;
	public Stat speed;
	public Stat defense;
	public List<GameObject> drops = new List<GameObject>();	
	public float healthDropChance;

	protected GameObject textBoxObject;
	private DamageComponent damager;

	public int maxCoinDrop = 3;
	public int minCoinDrop;
	public float stunDuration;

	public Buff[] activeEffects;

	public virtual void Damage(GameObject damager = (null), float amt = (1.0F), bool tickOnce = (false)){
		float totalDamage = amt - defense.GetTotal ();
		if (totalDamage <= 0)
			totalDamage = 0;
		health.Add (-totalDamage);	// Final Damage Calculation on All Entities

		if(health.GetTotal() <= 0){			
			Die ();
		}
	}


	public bool DetermineChance (float chance){
		float random = Random.Range (0, 100);
		// True if your critChance > 0 and if you landed on 100
		return (chance > 0) && (random <= chance);
	}
	/*
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject != gameObject) {
			if (other.GetComponent<DamageComponent> () != null) {
				DamageComponent damageComponent = other.GetComponent<DamageComponent> ();
				if (damageComponent.friendlyString != gameObject.name) {
					float dmg = damageComponent.dmg;
					if(damageComponent.DetermineCrit ())
						Damage (other.gameObject, dmg * damageComponent.critMultiplier, true);
					else
						Damage (other.gameObject, dmg, true);
				}
			}
		}
	}
	*/
	public void Die(){
		GameManager.kills++;
		if (creatureIndex != -1) {
			//Encyclopedia.Learn (creatureIndex);
		}
		// Remove references here
		//GameManager.score += 10;
		if(DetermineChance(healthDropChance)){
			Instantiate (GameManager.healthDropItem, transform.position, Quaternion.identity);
		}
			
		if (drops.Count > 0) {
			int rng = Random.Range (minCoinDrop, maxCoinDrop);
			for (int i = 0; i < rng; i++) {
				int rng_2 = Random.Range (0, drops.Count);
				Instantiate (drops [rng_2], transform.position, Quaternion.identity);
			}
		}
		Destroy (gameObject);
	}

	void Awake(){
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("entity").Length; i++) {
			otherEntitySprites.Add (GameObject.FindGameObjectsWithTag ("entity")[i].GetComponent<SpriteRenderer> ());
		}
	}

	public virtual void Start(){
		health.Init ();
		speed.Init ();
		textBoxObject = Resources.Load<GameObject> ("Prefabs/DamageInformation");
	}

	public virtual void Update(){
		GetComponent<SpriteRenderer> ().sortingOrder = -(int)(transform.position.y * 10) * heightLayer;
		stunDuration -= Time.deltaTime;

		if (health.GetTotal() <= 0) {
			Die ();
		}
	}
}
