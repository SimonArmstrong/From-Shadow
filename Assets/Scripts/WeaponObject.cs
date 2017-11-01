using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour {
	public Weapon item;

	[HideInInspector]
	public SpriteRenderer r;

	[HideInInspector]
	public Rigidbody2D rb;

	public void Start(){
		r = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D> ();

		r.sprite = item.sprite;
		name = item.name;
	}

	public void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Player> () != null) {
			Player player = other.GetComponent<Player> ();
			player.weapon = item;

			Destroy (gameObject);
		}
	}
}
