using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorekeeper {

	public SceneController sceneControler { get; set; }

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
	//hero的默认位置
	private int last = 1;
	//更改plane加分
	public void judge() {
		GameObject[] plane = sceneControler.plane;
		for (int i = 0; i < plane.Length; ++i) {
			if (Vector3.Distance (plane [i].transform.position, sceneControler.hero.transform.position) <= 2.8 && i != last) {
				last = i;
				++score;
				Debug.Log (score);
				break;
			}
		}
	}
}