using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper {

	public SceneController sceneController { get; set; }

	private static Scorekeeper _instance;
	public static Scorekeeper getInstance(){
		if (_instance == null)
			_instance = new Scorekeeper();
		return _instance;
	}

	public int score = 0;

	public void reset(){
		score = 0;
	}

	//更改plane加分
	public void judge() {
		GameObject[] plane = sceneController.plane;
		for (int i = 0; i < plane.Length; ++i) {
			if (Vector3.Distance (plane [i].transform.position, sceneController.hero.transform.position) <= 2.8) {
				if (i != sceneController.hero.GetComponent<MoveData> ().planeNum) {
					sceneController.hero.GetComponent<MoveData> ().planeNum = i;
					++score;
					Debug.Log (score);
				}
				return;
			}

		}
		//处于交界
		sceneController.hero.GetComponent<MoveData>().planeNum = -1;
	}
}