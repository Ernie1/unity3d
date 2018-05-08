using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : System.Object {
    private static PatrolFactory _instance;
    public SceneController sceneController { get; set; }
    public List<GameObject> used;
    public List<GameObject> free;

    public static PatrolFactory getInstance(){
        if (_instance == null) {
            _instance = new PatrolFactory();
            _instance.used = new List<GameObject>();
            _instance.free = new List<GameObject>();
        }
        return _instance;
    }

    public GameObject getPatrol() {

        GameObject newPatrol;
        if (free.Count == 0)
			newPatrol = GameObject.Instantiate(Resources.Load("prefabs/Patrol")) as GameObject;
        else {
            newPatrol = free[0];
            free.Remove(free[0]);
        }
		newPatrol.SetActive(true);
        used.Add(newPatrol);
		if (!newPatrol.GetComponent<Rigidbody> ()) {
			newPatrol.AddComponent<Rigidbody> ();  
		}
        return newPatrol;
    }

    public void freePatrol(GameObject g) {
        for (int i = 0; i < used.Count; i++) {
            if (used[i] == g) {
                used.Remove(g);
                g.SetActive(false);
                free.Add(g);
            }
        }
    }

	public void hideAll() {
		for (int i = 0; i < used.Count; i++)
			used [i].SetActive (false);
		for (int i = 0; i < free.Count; i++)
			free [i].SetActive (false);
	}
}