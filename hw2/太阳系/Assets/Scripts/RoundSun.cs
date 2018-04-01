using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSun :MonoBehaviour {
	public Transform sun;
	public Transform mercury;
	public Transform venus;
	public Transform earth;
	public Transform earthShadow;
	public Transform moon;
	public Transform mars;
	public Transform jupiter;
	public Transform saturn;
	public Transform uranus;
	public Transform neptune;

	private Vector3 ax1, ax2, ax3, ax4 , ax5, ax6, ax7;

	void Start(){
		ax1 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
		ax2 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
		ax3 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
		ax4 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
		ax5 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
		ax6 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
		ax7 = new Vector3(Random.Range(1,360),Random.Range(1,360),Random.Range(1,360));
	}
	void Update () {
		mercury.RotateAround (sun.position, ax1, 120 * Time.deltaTime);
		venus.RotateAround (sun.position, ax2, 110 * Time.deltaTime);
		earth.RotateAround(sun.position, new Vector3(0,2,0),100 * Time.deltaTime) ;
		earth.Rotate (Vector3.up * 30 * Time.deltaTime);
		moon.transform.RotateAround(earthShadow.position,Vector3.up,359 * Time.deltaTime);
		mars.RotateAround (sun.position, ax3, 90 * Time.deltaTime);
		jupiter.RotateAround (sun.position, ax4, 80 * Time.deltaTime);
		saturn.RotateAround (sun.position, ax5, 70 * Time.deltaTime);
		uranus.RotateAround (sun.position, ax6, 60 * Time.deltaTime);
		neptune.RotateAround (sun.position, ax7, 50 * Time.deltaTime);
	}
}