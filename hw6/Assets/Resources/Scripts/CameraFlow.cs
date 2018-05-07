//https://blog.csdn.net/u011484013/article/details/51554745
using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour {
	public Transform target;
	private Vector3 offset;
	// Use this for initialization
	void Start() {
		offset = target.position - this.transform.position;
	}

	// Update is called once per frame
	void Update() {
		this.transform.position = target.position - offset;
	}
}