using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : System.Object {
	
    private static DiskFactory _instance;
    public SceneController sceneControler { get; set; }
    public List<GameObject> used;
    public List<GameObject> free;

    public static DiskFactory getInstance(){
        if (_instance == null) {
            _instance = new DiskFactory();
            _instance.used = new List<GameObject>();
            _instance.free = new List<GameObject>();
        }
        return _instance;
    }

    public GameObject getDisk() {

        GameObject newDisk;
        if (free.Count == 0)
            newDisk = GameObject.Instantiate(Resources.Load("prefabs/Disk")) as GameObject;
        else {
            newDisk = free[0];
            free.Remove(free[0]);
        }
		newDisk.SetActive(true);
        used.Add(newDisk);
        return newDisk;
    }

    public void freeDisk(GameObject g) {
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