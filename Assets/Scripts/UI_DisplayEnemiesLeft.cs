﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DisplayEnemiesLeft : MonoBehaviour {
	public Text textbox;
	// Use this for initialization
	void Start () {
		textbox = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		textbox.text = (GameManager.enemiesThisWave - GameManager.enemyCount).ToString ();
	}
}
