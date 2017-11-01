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

[CreateAssetMenu(fileName="Item")]
public class Item : ScriptableObject {
	//
	// BASIC STATISTICS
	//

	public Sprite sprite;
	public string name;
	public string description;

	public float value;

	public KnowledgeRating rarity {
		get;
		set;
	}
}
