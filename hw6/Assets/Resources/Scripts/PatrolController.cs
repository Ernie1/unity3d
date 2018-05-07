using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour {
	void OnCollisionStay(Collision e) {
		if (!e.gameObject.name.Contains ("Plane") && !e.gameObject.name.Contains ("env_crete1b")) {
			Debug.Log (e.gameObject.name);
		}
	}
}