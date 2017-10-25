using UnityEngine;
using System.Collections;

public enum KnowledgeRating {
	BASIC,
	APPRENTICE,
	SCHOLARLY,
	SUPERIOR,
	MASTERFUL,
	TRANSCENDENT
}

public class Item : MonoBehaviour {
	//
	// BASIC STATISTICS
	//
	public float value { 
		get; 
		set;
	}

	public KnowledgeRating rarity {
		get;
		set;
	}
}
