using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class table : MonoBehaviour {

//	void Awake () {
//		Debug.Log ("Table Awake");
//	}
//		
	void res(){
		Debug.Log (this.gameObject.name);
	}
	void Start () {
		this.BroadcastMessage ("res");
	}
//		
//	void Update () {
//		Debug.Log ("Table Update");
//	}
//
//	void FixedUpdate () {
//		Debug.Log ("Table FixedUpdate");
//	}
//	
//	void LateUpdate () {
//		Debug.Log ("Table LateUpdate");
//	}
//
//	void OnGUI () {
//		Debug.Log ("Table OnGUI");
//	}
//
//	void Reset () {
//		Debug.Log ("Table Reset");
//	}
//
//	void OnEnable () {
//		Debug.Log ("Table OnEnable");
//	}
//
//	void OnDisable () {
//		Debug.Log ("Table OnDisable");
//	}
//
//	void OnDestroy () {
//		Debug.Log ("Table OnDestroy");
//	}

}
