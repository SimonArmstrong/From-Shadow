using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingComponent : MonoBehaviour {
	public float damage;
	public float critChance;
	public float critMultiplier;
	public float swingForce;
	public float swingStep;
	public float stunDuration;

	float baseDamage;
	float baseCritChance;
	float baseCritMultiplier;
	float baseSwingForce;
	float baseSwingStep;
	float baseStunDuration;  

	public Weapon weapon;

	public bool DetermineCrit(){
		float random = Random.Range (0, 100);
		// True if your critChance > 0 and if you landed on 100
		return (critChance > 0) && (random <= critChance);
	}

	public void LoadWeaponStats(Weapon w){
		weapon = w;

		critChance = baseCritChance;
		critMultiplier = baseCritMultiplier;
		swingForce = baseSwingForce;
		swingStep = baseSwingStep;

		critChance *= weapon.critChance;
		critMultiplier *= weapon.critDamage;
		swingForce *= weapon.knockback;
		swingStep *= weapon.stepForce;

	}

	void Awake(){
		baseDamage = damage;
		baseCritChance = critChance;
     	baseCritMultiplier = critMultiplier;
     	baseSwingForce = swingForce;
     	baseSwingStep = swingStep;
     	baseStunDuration = stunDuration;
	}

	public float GetDamageOutput(){
		float result = damage * weapon.damage;
		if (DetermineCrit ())
			result = result * critMultiplier;

		return result;
	}
}
