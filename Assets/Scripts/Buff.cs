using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff")]
public class Buff : ScriptableObject {
	public enum Type {
		STATDOWN,
		STATUP,
		STATFREEZE
	}
	public float amount;
	public float duration;
}
