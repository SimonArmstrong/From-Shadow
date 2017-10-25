using UnityEngine;
using System.Collections;

public class RoomObject : MonoBehaviour {

	public bool up;
	public bool down;
	public bool left;
	public bool right;

	public bool bossRoom;

	public SpriteRenderer renderer;

	public GameObject doorSlotTileUp;
	public GameObject doorSlotTileDown;
	public GameObject doorSlotTileLeft;
	public GameObject doorSlotTileRight;

	public GameObject rightWall;
	public GameObject leftWall;

	// Use this for initialization
	public void Init () {
		if (up && down && left && right) {
		}

		if (bossRoom) {
			Debug.Log (gameObject.name);
		}

		if (up) {
			Destroy (doorSlotTileUp);
		}
		if (down) {
			Destroy (doorSlotTileDown);
		}
		if (left) {
			Destroy (doorSlotTileLeft);
		}
		if (right) {
			Destroy (doorSlotTileRight);
		}

		//renderer.sortingOrder = (int)transform.position.y;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
