using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GhostMeter : MonoBehaviour {
	public Slider parentSlider;
	private Slider slider;
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		slider.maxValue = parentSlider.maxValue;
		slider.value = parentSlider.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (parentSlider.value < slider.value) {
			slider.value = Mathf.Lerp(slider.value, parentSlider.value, Time.deltaTime * 2);
		}
	}
}
