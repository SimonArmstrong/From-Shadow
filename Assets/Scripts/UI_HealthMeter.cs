using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthMeter : MonoBehaviour {
	private Slider healthSlider;
	public Entity owner;
	public Player player_owner;
	// Use this for initialization
	void Start () {
		if (player_owner != null) {
			healthSlider = GetComponent<Slider> ();
			healthSlider.maxValue = player_owner.health.max;
		} else {
			healthSlider = GetComponent<Slider> ();
			healthSlider.maxValue = owner.health.max;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player_owner != null) {
			healthSlider.value =  player_owner.health.GetCurrent();
		} else {
			healthSlider.value =  owner.health.cur;
		}
	}
}
