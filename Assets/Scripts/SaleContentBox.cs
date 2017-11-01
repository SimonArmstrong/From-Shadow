using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleContentBox : MonoBehaviour {
	public Text displayNameTextBox;
	public Text nameDispShadow;
	public Image iconDisplay;
	public Image iconDisplayShadow;
	public Text attackDisp;
	public Text feroDisp;
	public Text precDisp;
	public Text stepDisp;
	public Text knockDisp;
	public Text valueDisp;
	public Text valueDispShadow;

	public Item item;

	// Use this for initialization
	public void Load () {
		displayNameTextBox.text = item.name;
		nameDispShadow.text = item.name;
		attackDisp.text = ((Weapon)item).damage.ToString();
		feroDisp.text = ((Weapon)item).critDamage.ToString();
		precDisp.text = ((Weapon)item).critChance.ToString();
		stepDisp.text = ((Weapon)item).stepForce.ToString();
		knockDisp.text = ((Weapon)item).knockback.ToString();

		valueDisp.text = item.value.ToString();
		valueDispShadow.text = item.value.ToString();

		iconDisplay.sprite = item.sprite;
		iconDisplayShadow.sprite = item.sprite;
	}
}
