using UnityEngine;
using System.Collections;

public class DamageComponent : MonoBehaviour {
	public float dmg;
	public float dmgDelay = 0.2f;
	public string friendlyString;
	public float critChance;
	public float critMultiplier = 2;

	public bool DetermineCrit(){
		float random = Random.Range (0, 100);
		// True if your critChance > 0 and if you landed on 100
		return (critChance > 0) && (random <= critChance);
	}
}
