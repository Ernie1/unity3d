using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction  
{  
	void Restart ();
	void GameOver ();
}

public class UserGUI : MonoBehaviour {

	private IUserAction action;

	void Start () {

	}

	void Update () {

	}

	void OnGUI () {
		float width = Screen.width / 6;
		float height = Screen.height / 12;
		if (GUI.Button (new Rect (0, 0, width, height), "Game Over!")) {
			action.GameOver ();
		}
	}

}