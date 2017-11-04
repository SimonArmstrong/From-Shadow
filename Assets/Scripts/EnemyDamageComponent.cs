using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageComponent : MonoBehaviour {
	public float dmg;
	public float dmgDelay = 0.2f;
	public string friendlyString;
	public float critChance;
	public float critMultiplier = 2;
}
