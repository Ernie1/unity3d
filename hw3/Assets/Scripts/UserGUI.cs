using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{  
    void Restart ();
    void GameOver ();
    void Pause ();
    void Resume ();
    bool checkObjectClicked ();
}

public class UserGUI : MonoBehaviour {

    public IUserAction action;
    public int status;

    void Awake () {

    }

    void Start () {

    }

    void Update () {

    }

    void OnGUI () {
        float width = Screen.width / 6;
        float height = Screen.height / 12;
        if (status == 0) {
            action.GameOver ();
            if (GUI.Button (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height), "Game Over!\nRestart")) {
                action.Restart ();
            }
        }
        if (status == 1) {
            action.GameOver ();
            if (GUI.Button (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height), "Win!\nRestart")) {
                action.Restart ();
            }
        }
        if (status == 2) {
            if (GUI.Button (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height), "Pause")) {
                action.Pause ();
            }
            if (GUI.Button (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2 + height, width, height), "Restart")) {
                action.Restart ();
            }
        }
        if (status == 3) {
            if (GUI.Button (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height), "Resume")) {
                action.Resume ();
            }
            if (GUI.Button (new Rect (Screen.width / 2 - width / 2, Screen.height / 2 - height / 2 + height, width, height), "Restart")) {
                action.Restart ();
            }
        }
    }

}