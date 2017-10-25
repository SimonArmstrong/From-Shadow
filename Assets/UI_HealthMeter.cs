using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthMeter : MonoBehaviour {
	private Slider healthSlider;
	public Entity owner;
	// Use this for initialization
	void Start () {
		healthSlider = GetComponent<Slider> ();
		healthSlider.maxValue = owner.health.max;
	}
	
	// Update is called once per frame
	void Update () {
		healthSlider.value =  owner.health.cur;
	}
}
