using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUserAction{
	int getScore ();
	bool isGameOver ();
}

public class UserGUI : MonoBehaviour{
    
	private IUserAction action;
	public Text scoreText;
	public Text gameOver;

    void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
	}

    void OnGUI() {
		scoreText.text = "Score: " + action.getScore ();
		if (action.isGameOver ())
			gameOver.text = "Game Over";
		else
			gameOver.text = "";
    }
}