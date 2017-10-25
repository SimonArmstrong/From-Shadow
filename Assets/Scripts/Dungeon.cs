using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Dungeon : MonoBehaviour {
#region Room Structure
	public class Room {
		public static Vector2 distanceApart;
		public Vector3 position;
		public Room up;
		public Room down;
		public Room left;
		public Room right;

		public bool isBossRoom;

		private int doors;

		public int DoorCount(){
			return doors;
		}

		public Room(Vector3 v){
			position = v;

			up = null;
			down = null;
			left = null;
			right = null;
		}

		public Room(Vector3 v, Room u, Room d, Room l, Room r){
			position = v;
			if(u != null)
			up = u;
			if(d != null)
			down = d;
			if(l != null)
			left = l;
			if(r != null)
			right = r;
		}
	}
#endregion

	public GameObject roomObject;
	public bool randomizeSeed;
	public int seed;

	public List<Room> rooms = new List<Room>();
	public Room startRoom;
	public int maxRooms;
	public Vector2 distanceApart;

	void BuildDungeon (){
		for (int i = 0; i < rooms.Count; i++) {
			GameObject toSpawn = Instantiate (roomObject, rooms [i].position, Quaternion.identity) as GameObject;

			if (rooms [i].up != null)
				toSpawn.GetComponent<RoomObject> ().up = true;
			if (rooms [i].down != null)
				toSpawn.GetComponent<RoomObject> ().down = true;
			if (rooms [i].left != null)
				toSpawn.GetComponent<RoomObject> ().left = true;
			if (rooms [i].right != null)
				toSpawn.GetComponent<RoomObject> ().right = true;
			if (rooms [i].isBossRoom)
				toSpawn.GetComponent<RoomObject> ().bossRoom = true;

			toSpawn.GetComponent<RoomObject> ().Init ();
		}
	}

	Room GetRoomFromPosition(Vector3 pos){
		Room result = null;
		for (int i = 0; i < rooms.Count; i++) {
			if (rooms [i].position == pos)
				result = rooms [i];
		}
		return result;
	}

	void MakeBranch(int branchSize){
		int roomCount = 0;
		int index = 0;

		bool[] rand_doors = new bool[4];

		while (roomCount < branchSize) {
			int corridoorSize = Random.Range (1, (int)(branchSize));
			int falseCount = 0;
			for (int i = 0; i < rand_doors.Length; i++) {
				rand_doors [i] = (Random.Range (0, 2) > 0);
				if (!rand_doors [i]) {
					falseCount++;
					if (falseCount > 3) {
						rand_doors [0] = true;
					}
				}
				if (rand_doors [i]) {
					if (i == 0) {
						for (int j = 0; j < corridoorSize; j++) {
							Vector3 desiredPos = (rooms [index].position + Vector3.up * (distanceApart.y));

							if (GetRoomFromPosition (desiredPos) != null) {
								rooms [index].up = GetRoomFromPosition (desiredPos);
							} else {
								rooms.Add (new Room (desiredPos, null, rooms [index], null, null));
								rooms [index].up = rooms [index + 1];
								roomCount++;
							}
						}
					}
					if (i == 1) {
						for (int j = 0; j < corridoorSize; j++) {
							Vector3 desiredPos = (rooms [index].position + Vector3.down * (distanceApart.y));
							if (GetRoomFromPosition (desiredPos) != null) {
								rooms [index].down = GetRoomFromPosition (desiredPos);
							} else {
								rooms.Add (new Room (desiredPos, rooms [index], null, null, null));
								rooms [index].down = rooms [index + 1];
								roomCount++;
							}
						}
					}
					if (i == 2) {
						for (int j = 0; j < corridoorSize; j++) {
							Vector3 desiredPos = (rooms [index].position + Vector3.left * (distanceApart.x));
							if (GetRoomFromPosition (desiredPos) != null) {
								rooms [index].left = GetRoomFromPosition (desiredPos);
							} else {
								rooms.Add (new Room (desiredPos, null, null, null, rooms [index]));
								rooms [index].left = rooms [index + 1];
								roomCount++;
							}
						}
					}
					if (i == 3) {
						for (int j = 0; j < corridoorSize; j++) {
							Vector3 desiredPos = (rooms [index].position + Vector3.right * (distanceApart.x));
							if (GetRoomFromPosition (desiredPos) != null) {
								rooms [index].right = GetRoomFromPosition (desiredPos);
							} else {
								rooms.Add (new Room (desiredPos, null, null, rooms [index], null));
								rooms [index].right = rooms [index + 1];
								roomCount++;
							}
						}
					}
				}
			}
			index++;
		}
	}

	void SetBossRoom(){
		float tempFurthest = 0;
		int furthestIndex = 0;
		for (int i = 0; i < rooms.Count; i++) {
			if (rooms [i].position.magnitude > tempFurthest) {
				tempFurthest = rooms [i].position.magnitude;
				furthestIndex = i;
			}
		}

		rooms [furthestIndex].isBossRoom = true;
	}

	// Use this for initialization
	void Start () {
		if(randomizeSeed)
			seed = Random.Range (0, 99999);
		
		Random.seed = seed;
		Room.distanceApart = distanceApart;
		// Make Start room
		rooms.Add (new Room(Vector3.zero));
		startRoom = rooms [0];

		MakeBranch (maxRooms);
		SetBossRoom ();

		BuildDungeon ();
	}

	// Update is called once per frame
	void Update () {

	}
}
