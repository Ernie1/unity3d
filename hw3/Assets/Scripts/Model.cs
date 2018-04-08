using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model {
    private static Model _instance;

    private GameObject bankHere, bankThere, boat, water;
    private GameObject []priest, devil;

    public Vector3[] boatPos, onBoat;
    public Vector3[,] priestPos, devilPos;
	public Vector3[] bankSharp;

    public static Model getInstance()  
    {  
        if (_instance == null)  
        {  
            _instance = new Model();  
        }  
        return _instance;  
    }

    public void GenGameObjects () {
        boatPos = new Vector3[]{ new Vector3 (2, 0.1F, 0), new Vector3 (-2, 0.1F, 0) };
        onBoat = new Vector3[]{ new Vector3 (-1.5F, 2, 0), new Vector3 (1.5F, 2, 0) };
        priestPos = new Vector3[3, 2];
        devilPos = new Vector3[3, 2];
		bankSharp = new Vector3[]{ new Vector3 (3, 1, 0), new Vector3 (-3, 1, 0) };
        for (int i = 0; i < 3; ++i) {
            priestPos [i, 0] = new Vector3 (3.4F + 0.7F * i, 1, 0);
            priestPos [i, 1] = new Vector3 (-6.9F + 0.7F * i, 1, 0);
            devilPos [i, 0] = new Vector3 (5.5F + 0.7F * i, 1, 0);
            devilPos [i, 1] = new Vector3 (-4.8F + 0.7F * i, 1, 0);
        }
        bankHere = GameObject.Instantiate (Resources.Load ("Prefabs/bank"), new Vector3 (5.25F, 0, 0), Quaternion.identity, null) as GameObject;
        bankHere.name = "bankHere";
        bankThere = GameObject.Instantiate (Resources.Load ("Prefabs/bank"), new Vector3 (-5.25F, 0, 0), Quaternion.identity, null) as GameObject;
        bankThere.name = "bankThere";
        boat = GameObject.Instantiate (Resources.Load ("Prefabs/boat"), boatPos [0], Quaternion.identity, null) as GameObject;
        boat.name = "boat";
        water = GameObject.Instantiate (Resources.Load ("Prefabs/water"), new Vector3 (0, -0.25F, 0), Quaternion.identity, null) as GameObject;
        water.name = "water";
        priest = new GameObject[3];
        devil = new GameObject[3];
        for (int i = 0; i < 3; ++i) {
            priest [i] = GameObject.Instantiate (Resources.Load ("Prefabs/priest"), priestPos [i, 0], Quaternion.identity, null) as GameObject;
            priest [i].name = "priest" + i.ToString ();
            devil [i] = GameObject.Instantiate (Resources.Load ("Prefabs/devil"), devilPos [i, 0], Quaternion.identity, null) as GameObject;
            devil [i].name = "devil" + i.ToString ();
        }
    }

    public void destroy () {
        GameObject.Destroy (bankHere);
        GameObject.Destroy (bankThere);
        GameObject.Destroy (boat);
        GameObject.Destroy (water);
        foreach (GameObject e in priest)
            GameObject.Destroy (e);
        foreach (GameObject e in devil)
            GameObject.Destroy (e);
    }

    // 此岸 0，彼岸 1，否则 2
    public int whereBoat () {
        for (int i = 0; i < 2; ++i)
            if (boat.transform.position == boatPos [i])
                return i;
        return 2;
    }

    public int wherePriest (int e) {
        for (int i = 0; i < 2; ++i)
            if (priest [e].transform.position == priestPos [e, i])
                return i;
        return 2;
    }

    public int whereDevil (int e) {
        for (int i = 0; i < 2; ++i)
            if (devil [e].transform.position == devilPos [e, i])
                return i;
        return 2;
    }

    public int whichPriest (ref GameObject g) {
        return Array.IndexOf (priest, g);
    }

    public int whichDevil (ref GameObject g) {
        return Array.IndexOf (devil, g);
    }

    public int whichSeatByPosition (ref GameObject g) {
        if (g.transform.localPosition == onBoat [0])
            return 0;
        if (g.transform.localPosition == onBoat [1])
            return 1;
        return -1;
    }

	public Vector3 seatGlobalPos (int seatIndex) {
		return boat.transform.TransformPoint (onBoat [seatIndex]);
	}

	public void ashore (ref GameObject Object) {
		Object.transform.parent = null;
	}

	public void aboard (ref GameObject Object) {
		Object.transform.parent = boat.transform;
	}

    public bool isBoat (ref GameObject Object) {
        return Object == boat;
    }
}