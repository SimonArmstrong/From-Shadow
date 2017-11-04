using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeShopMenu : MonoBehaviour {
	public GameObject slotPrefab;

	public List<Item> stock = new List<Item> ();
	public float padding = 10;
	public float iconScale = 160;
	// Use this for initialization
	void Start () {
		((RectTransform)transform.parent).offsetMin = new Vector2 (0, -(stock.Count * iconScale + (stock.Count * padding)));
		for (int i = 0; i < stock.Count; i++) {
			GameObject slot = Instantiate (slotPrefab, Vector2.zero, Quaternion.identity) as GameObject;
			slot.GetComponent<SaleContentBox> ().item = stock [i];
			slot.GetComponent<SaleContentBox> ().Load ();
			slot.transform.SetParent (transform);
			slot.transform.localScale = new Vector3(1, 1, 1);


			((RectTransform)slot.transform).offsetMin = new Vector2 (-580, -160);
			((RectTransform)slot.transform).offsetMax = new Vector2 (-580, 0);
			((RectTransform)slot.transform).anchoredPosition = new Vector2 (0, -iconScale * i - i*padding);
		}
	}
}
