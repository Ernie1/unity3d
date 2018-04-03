using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
	
	private GameObject bankHere, bankThere, boat, water;
	private GameObject []priest, devil;

	private GameObject objectClicked;

	Vector3[] boatPos, onBoat;
	Vector3[,] priestPos, devilPos;

	bool[] boatSeat;

	private bool someObjectHandling;
	private int handleNum;
	private int sailTo;
	private int boatSeatIndex;
	private Vector3 jumpFrom,jumpTo;
	float dTime;
	Vector3 speed, Gravity;

	void Awake () {
		boatPos = new Vector3[]{ new Vector3 (2, 0.1F, 0), new Vector3 (-2, 0.1F, 0) };
		onBoat = new Vector3[]{ new Vector3 (-1.5F, 2, 0), new Vector3 (1.5F, 2, 0) };
		priestPos = new Vector3[3, 2];
		devilPos = new Vector3[3, 2];
		for(int i=0;i<3;++i){
			priestPos [i, 0] = new Vector3 (3.4F + 0.7F * i, 1, 0);
			priestPos [i, 1] = new Vector3 (-6.9F + 0.7F * i, 1, 0);
			devilPos [i, 0] = new Vector3 (5.5F + 0.7F * i, 1, 0);
			devilPos [i, 1] = new Vector3 (-4.8F + 0.7F * i, 1, 0);
		}
		boatSeat = new bool[]{ true, true };
		someObjectHandling = false;

		SSDirector director = SSDirector.getInstance ();
		director.currentScenceController = this;
		director.setFPS (60);
		director.currentScenceController.LoadResources ();
	}

	public void LoadResources () {
		bankHere = Instantiate (Resources.Load ("Prefabs/bank"), new Vector3 (5.25F, 0, 0), Quaternion.identity, null) as GameObject;
		bankHere.name = "bankHere";
		bankThere = Instantiate (Resources.Load ("Prefabs/bank"), new Vector3 (-5.25F, 0, 0), Quaternion.identity, null) as GameObject;
		bankThere.name = "bankThere";
		boat = Instantiate (Resources.Load ("Prefabs/boat"), boatPos [0], Quaternion.identity, null) as GameObject;
		boat.name = "boat";
		water = Instantiate (Resources.Load ("Prefabs/water"), new Vector3 (0, -0.25F, 0), Quaternion.identity, null) as GameObject;
		water.name = "water";
		priest = new GameObject[3];
		devil = new GameObject[3];
		for (int i = 0; i < 3; ++i) {
			priest [i] = Instantiate (Resources.Load ("Prefabs/priest"), priestPos [i, 0], Quaternion.identity, null) as GameObject;
			priest [i].name = "priest" + i.ToString ();
			devil [i] = Instantiate (Resources.Load ("Prefabs/devil"), devilPos [i, 0], Quaternion.identity, null) as GameObject;
			devil [i].name = "devil" + i.ToString ();
		}
	}

	//船在此岸 0，彼岸 1，否则 2
	int whereBoat () {
		for (int i = 0; i < 2; ++i)
			if (boat.transform.position == boatPos [i])
				return i;
		return 2;
	}

	int wherePriest (int e) {
		for (int i = 0; i < 2; ++i)
			if (priest [e].transform.position == priestPos [e, i])
				return i;
		return 2;
	}

	int whereDevil (int e) {
		for (int i = 0; i < 2; ++i)
			if (devil [e].transform.position == devilPos [e, i])
				return i;
		return 2;
	}

	public void Pause () {

	}

	public void Resume () {

	}

	public void Restart () {

	}

	public void GameOver () {
		//SSDirector.getInstance().
	}

	// Use this for initialization
	void Start () {

	}

	bool checkObjectClicked(){
		if(Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit ;
			if(Physics.Raycast (ray,out hit)) {
				objectClicked = hit.collider.gameObject;
				someObjectHandling = true;
				return true;
			}
		}
		return false;
	}

	void handleClickedObject () {
		handleNum = -1;
		//boat
		if (objectClicked == boat) {
			if (someoneOnBoat ()) {				
				handleNum = 0;
				sailTo = 1 - whereBoat ();
			} else
				someObjectHandling = false;
			return;
		}
//		startTime = Time.time;
		jumpFrom = objectClicked.transform.position;
		//priest
		int index = whichPriest(objectClicked);
		if (index != -1) {
			if (wherePriest (index) == 2) {
				boatSeatIndex = whichSeatByPosition (objectClicked.transform.localPosition);
				jumpTo = priestPos [index, whereBoat ()];
				setJump ();
				handleNum = 1;
			} else if (wherePriest (index) == whereBoat () && whereCanSit () != 2) {
				jumpTo = boat.transform.TransformPoint (onBoat [whereCanSit ()]);
				setJump ();
				handleNum = 2;
			} else
				someObjectHandling = false;
			return;
		}
		//devil
		index = whichDevil(objectClicked);
		if (index != -1) {
			if (whereDevil (index) == 2) {
				boatSeatIndex = whichSeatByPosition (objectClicked.transform.localPosition);
				jumpTo = devilPos [index, whereBoat ()];
				setJump ();
				handleNum = 1;
			}
			else if (whereDevil (index) == whereBoat () && whereCanSit () != 2) {
				jumpTo = boat.transform.TransformPoint (onBoat [whereCanSit ()]);
				setJump ();
				handleNum = 2;
			} else
				someObjectHandling = false;
			return;
		}
		//其它不管
		someObjectHandling = false;
	}

	void sail(){
		float speed = 1.5F;
		boat.transform.position = Vector3.MoveTowards (boat.transform.position, boatPos [sailTo], speed * Time.deltaTime);
		if (boat.transform.position == boatPos [sailTo])
			someObjectHandling = false;
	}

	int whichPriest(GameObject g){
		return Array.IndexOf (priest, g);
	}

	int whichDevil(GameObject g){
		return Array.IndexOf (devil, g);
	}

	int whereCanSit() {
		if (boatSeat [0])
			return 0;
		if (boatSeat [1])
			return 1;
		return 2;
	}

	bool someoneOnBoat () {
		if(boatSeat[0]&&boatSeat[1])
			return false;
		return true;
	}

	int whichSeatByPosition(Vector3 p){
		if (p == onBoat [0])
			return 0;
		if (p == onBoat [1])
			return 1;
		return -1;
	}

	void aboard () {
		jump ();
		if (!someObjectHandling) {
			objectClicked.transform.parent = boat.transform;
			boatSeat [whereCanSit ()] = false;
		}
	}

	void ashore () {
		jump ();
		if (!someObjectHandling) {
			objectClicked.transform.parent = this.transform;
//		int index = whichPriest (objectClicked);
//		if (index != -1)
//			objectClicked.transform.position = priestPos [index, whichBank];
//		index = whichDevil (objectClicked);
//		if (index != -1)
//			objectClicked.transform.position = devilPos [index, whichBank];
			boatSeat [boatSeatIndex] = true;
		}
	}

	void setJump(){
		float ShotSpeed = 10; // 抛出的速度
		float time = Vector3.Distance (jumpFrom, jumpTo) / ShotSpeed;
		float g = -10;
		dTime = 0;
		speed = new Vector3 ((jumpTo.x - jumpFrom.x) / time, (jumpTo.y - jumpFrom.y) / time - 0.5f * g * time, (jumpTo.z - jumpFrom.z) / time);
		Gravity = Vector3.zero;
	}

	void jump() {
//		float journeyTime = 1.0F;
//		Vector3 center = (jumpFrom + jumpTo) * 0.5F;
//		center -= new Vector3 (0, 1, 0);
//		Vector3 riseRelCenter = -center;
//		Vector3 setRelCenter = jumpTo - center;
//		float fracComplete = (Time.time - startTime) / journeyTime;
//		objectClicked.transform.position = Vector3.Slerp (riseRelCenter, setRelCenter, fracComplete);
//		objectClicked.transform.position += center;

		float g = -10;
		Gravity.y = g * (dTime += Time.fixedDeltaTime);// v=gt
		objectClicked.transform.position += (speed + Gravity) * Time.fixedDeltaTime;//模拟位移
//		Vector3 currentAngle = new Vector3();
//		currentAngle.x = -Mathf.Atan((speed.y + Gravity.y) / speed.z) * Mathf.Rad2Deg;// 弧度转度：Mathf.Rad2Deg
//		objectClicked.transform.eulerAngles = currentAngle;// 设置当前角度
		if (objectClicked.transform.position.x - jumpTo.x < 0.5) {
			objectClicked.transform.position = jumpTo;
			someObjectHandling = false;
		}
	}

	//0 lose 1 win 2 -
	bool checkGame () {
		int priestHere = 0, priestThere = 0, devilHere = 0, devilThere = 0;
		for(int i=0;i<3;++i){
			if (wherePriest (i) == 0)
				++priestHere;
			else if (wherePriest (i) == 1)
				++priestThere;
			else if (whereBoat () != 2) {
				if (whereBoat () == 0)
					++priestHere;
				else
					++priestThere;
			}
			if (whereDevil (i) == 0)
				++devilHere;
			else if (whereDevil (i) == 1)
				++devilThere;
			else if (whereBoat () != 2) {
				if (whereBoat () == 0)
					++devilHere;
				else
					++devilThere;
			}
		}
		if ((priestHere > 0 && priestHere < devilHere) || (priestThere > 0 && priestThere < devilThere))
			return 0;
		if (priestThere == 3 && devilThere == 3)
			return 1;
		return 2;
	}

	// Update is called once per frame
	void Update () {
		if (!someObjectHandling && checkObjectClicked ())
			handleClickedObject ();
		if (someObjectHandling) {
			if (handleNum == 0)
				sail ();
			else if (handleNum == 1)
				ashore ();
			else if (handleNum == 2)
				aboard ();
		}
		if (checkGame () == 0)
			Debug.Log ("lose");
	}
}