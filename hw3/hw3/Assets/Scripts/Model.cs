using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model {
    private static Model _instance;

    private GameObject bankHere, bankThere, boat, water;
    private GameObject []priest, devil;

    Vector3[] boatPos, onBoat;
    Vector3[,] priestPos, devilPos;

    Vector3 jumpFrom,jumpTo;

    float dTime;
    Vector3 speed, Gravity;

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

    void setJump(){
        float ShotSpeed = 10; // 抛出的速度
        float time = Vector3.Distance (jumpFrom, jumpTo) / ShotSpeed;
        float g = -10;
        dTime = 0;
        speed = new Vector3 ((jumpTo.x - jumpFrom.x) / time, (jumpTo.y - jumpFrom.y) / time - 0.5f * g * time, (jumpTo.z - jumpFrom.z) / time);
        Gravity = Vector3.zero;
    }

    public void setJumpFromObject (ref GameObject Object) {
        jumpFrom = Object.transform.position;
        setJump ();
    }

    public void setJumpToSeat (int seatIndex) {
        jumpTo = boat.transform.TransformPoint (onBoat [seatIndex]);
        setJump ();
    }

    public void setJumpToPriest (int index) {
        jumpTo = priestPos [index, whereBoat ()];
        setJump ();
    }

    public void setJumpToDevil (int index) {
        jumpTo = devilPos [index, whereBoat ()];
        setJump ();
    }

    public bool sailing (int sailTo){
        float speed = 1.5F;
        boat.transform.position = Vector3.MoveTowards (boat.transform.position, boatPos [sailTo], speed * Time.deltaTime);
        if (boat.transform.position == boatPos [sailTo])
            //someObjectHandling = false;
            return false;
        return true;
    }

    // mode: 0 ashore 1 aboard
    public bool jumping (ref GameObject Object, int mode) {
        float g = -10;
        Gravity.y = g * (dTime += Time.fixedDeltaTime);// v=gt
        Object.transform.position += (speed + Gravity) * Time.fixedDeltaTime;//模拟位移
        if (Object.transform.position.x - jumpTo.x < 0.5) {
            Object.transform.position = jumpTo;
            if (mode == 0)
                Object.transform.parent = null;
            else
                Object.transform.parent = boat.transform;
            return false;
        }
        return true;
    }

    public bool isBoat (ref GameObject Object) {
        return Object == boat;
    }
}