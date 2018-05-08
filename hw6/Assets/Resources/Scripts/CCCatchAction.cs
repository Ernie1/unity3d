using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCCatchAction : SSAction {

	private GameObject target;

	public static CCCatchAction GetSSAction (GameObject target) {
		CCCatchAction action = ScriptableObject.CreateInstance<CCCatchAction> ();
		action.target = target;
		return action;
	}

	public override void FixedUpdate () {
		//朝向目标移动
		transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position);

		this.transform.position = Vector3.MoveTowards (this.transform.position, target.transform.position, 0.0002f);
		
		//判断结束运动
		if (target.GetComponent<MoveData> ().planeNum == -1) {
			Debug.Log ("fini");
			this.destroy = true;
			this.callback.SSActionEvent (this);
		}
	}

	public override void Start () {
		//TODO: something
	}
}