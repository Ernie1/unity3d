//https://blog.csdn.net/u011484013/article/details/51554745
using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour {
	public Transform target;
	private Vector3 offset;
	private Vector3 pos;
	public SceneController sceneController;
	public float speed = 2;

	// Use this for initialization
	void Start() {
		offset = target.position - this.transform.position;
		sceneController = (SceneController)SSDirector.getInstance ().currentSceneController;
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (sceneController.isGameOver ()) {
			pos = target.position + new Vector3(0,20,0);
			this.transform.position = Vector3.Lerp (this.transform.position, pos, speed * Time.deltaTime);//调整相机与玩家之间的距离
			Quaternion angel = Quaternion.LookRotation (target.position - this.transform.position);//获取旋转角度
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, angel, speed * Time.deltaTime);
		} else
			this.transform.position = target.position - offset;
	}
}

//using UnityEngine;
//using System.Collections;
//
//public class CameraFlow : MonoBehaviour {
//	private Vector3 offset = new Vector3(0,5,4);//相机相对于玩家的位置
//	public Transform target;
//	private Vector3 pos;
//
//	public float speed = 2;
//
//	// Use this for initialization
//	void Start () {
//		
//	}
//
//	// Update is called once per frame
//	void Update () {
//		pos = target.position + offset;
//		this.transform.position = Vector3.Lerp(this.transform.position, pos, speed*Time.deltaTime);//调整相机与玩家之间的距离
//		Quaternion angel = Quaternion.LookRotation(target.position - this.transform.position);//获取旋转角度
//		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, angel, speed * Time.deltaTime);
//
//	}
//}