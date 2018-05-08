using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUserAction{
	int getScore ();
	bool isSafe ();
	bool isGameOver ();
}

public class UserGUI : MonoBehaviour{
    
	private IUserAction action;
	public Text scoreText;
	public Text status;

    void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
	}

    void OnGUI() {
		scoreText.text = "Score: " + action.getScore ();
		if (action.isGameOver ()) {
			status.text = "Game Over";
			status.fontSize = 60;
		} else if (action.isSafe ()) {
			status.text = "Temporarily Safe";
			status.fontSize = 40;
		}
		else
			status.text = "";
    }
}