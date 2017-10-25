using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageInformation : MonoBehaviour {
	public float damage;
	public float dmgDelay = 0.2f;
	public float lifeTime = 2;

	//[HideInInspector]
	public Text mainText;
	[HideInInspector]
	public Text shadText;

	private float initTime;

	// Use this for initialization
	void OnEnable () {
		shadText = GetComponent<Text> ();
		mainText = transform.GetChild(0).GetComponent<Text> ();
		initTime = lifeTime;
	}

	// Update is called once per frame
	void Update () {
		mainText.text = damage.ToString ("0");
		shadText.text = damage.ToString ("0");

		lifeTime -= Time.deltaTime;

		float f = (lifeTime / initTime);
		Color newCol = new Color(f, f/2, 0, 1);
		Color newShadCol = new Color(0, 0, 0, f);
		mainText.color = newCol;
		shadText.color = newShadCol;

		transform.Translate (Vector2.up * f);

		if (lifeTime <= 0) {
			Destroy (gameObject);
		}
	}
}
