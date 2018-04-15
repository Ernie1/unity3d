using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper {

	private static Scorekeeper _instance;
	public static Scorekeeper getInstance(){
		if (_instance == null)
			_instance = new Scorekeeper();
		return _instance;
	}

	public int score;

	public void reset(){
		score = 0;
	}

	public void record(GameObject hit) {
		score += hit.GetComponent<DiskData> ().score;
	}
}