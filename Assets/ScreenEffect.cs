using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffect : MonoBehaviour {
	private Image whiteScreenEffect;
	private float alpha;
	// Use this for initialization
	void Start () {
		whiteScreenEffect = GetComponent<Image> ();
	}

	public void Multiply(){
		
	}

	public void Begin(){
		alpha = 1f;
	}

	// Update is called once per frame
	void Update () {
		alpha = Mathf.Lerp(alpha, 0, Time.deltaTime * 20);
		whiteScreenEffect.color = new Color (1f, 1f, 1f, alpha);
	}
}
