using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
	public float damage;
	public float speed;

	public float critChance;
	public float critDamage;

	public float knockback;
	public float stepForce;

	public GameObject[] comboObjects;
}
