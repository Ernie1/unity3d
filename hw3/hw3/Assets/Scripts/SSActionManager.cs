﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {
	
	private Dictionary <int, SSAction> actions = new Dictionary <int, SSAction> ();
	private List <SSAction> waitingAdd = new List <SSAction> ();
	private List <int> waitingDelete = new List <int> ();

	// Update is called once per frame
	protected void Update () {
		foreach (SSAction ac in waitingAdd)
			actions [ac.GetInstanceID ()] = ac;
		waitingAdd.Clear ();

		foreach (KeyValuePair <int,SSAction> kv in actions) {
			SSAction ac = kv.Value;
			if (ac.destroy) {
				waitingDelete.Add (ac.GetInstanceID ()); // release action
			} else if (ac.enable) {
				ac.Update (); // update aciton
			}
		}

		foreach (int key in waitingDelete) {
			SSAction ac = actions [key];
			actions.Remove (key);
			DestroyObject (ac);
		}
		waitingDelete.Clear ();
	}

	public void RunAction (GameObject gameObject, SSAction action, ISSActionCallBack manager) {
		action.gameobject = gameObject;
		action.transform = gameObject.transform;
		action.callback = manager;
		waitingAdd.Add (action);
		action.Start ();
	}

	// Use this for initialization
	protected void Start () {
	}
}