using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBoss : Enemy {
	public string[] remarks;
	public float stunBreakTime = 2;
	private float stunLockTimer = 2;

	// Use this for initialization
	public override void Start () {
		stunLockTimer = stunBreakTime;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (stunDuration > 0) {
			stunLockTimer -= Time.deltaTime;
			if (stunLockTimer <= 0) {
				stunDuration = 0;	// Break out of stun, get invuln for 2 seconds
				stunLockTimer = stunBreakTime;
			}
		} else {
			stunLockTimer = stunBreakTime;
		}
	}
}
