﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.up * Time.deltaTime;
	}
}