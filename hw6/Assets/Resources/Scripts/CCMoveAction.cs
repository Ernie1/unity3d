using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMoveAction : SSAction {

	private int newState;

	public static CCMoveAction GetSSAction (int newState) {
		CCMoveAction action = ScriptableObject.CreateInstance<CCMoveAction> ();
		action.newState = newState;
		return action;
	}

	public override void FixedUpdate () {
		// Debug.Log(Vector3.Distance(a.transform.position,transform.position));
		// 根据当前人物方向与上一次备份的方向计算出模型旋转的角度
		int rotateValue = (newState - gameobject.GetComponent<MoveData>().state) * 90;
		Vector3 transformValue = new Vector3();

		// 模型移动的位置数值
		switch (newState) {
		case 0:
			transformValue = Vector3.forward * Time.deltaTime;
			break;
		case 2:
			transformValue = (-Vector3.forward) * Time.deltaTime;
			break;
		case 3:
			transformValue = Vector3.left * Time.deltaTime;
			break;
		case 1:
			transformValue = (-Vector3.left) * Time.deltaTime;
			break;
		}

		transform.Rotate(Vector3.up, rotateValue);
		//确保只有4个方向 
		transform.rotation = Quaternion.Euler (0, newState * 90, 0);
		// 移动人物
		transform.Translate(transformValue * gameobject.GetComponent<MoveData>().moveSpeed, Space.World);

		gameobject.GetComponent<MoveData> ().state = newState;
		this.destroy = true;
		this.callback.SSActionEvent (this);
	}

	public override void Start () {
		//TODO: something
	}
}