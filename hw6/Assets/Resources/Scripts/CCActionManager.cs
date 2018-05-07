using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {

	public SceneController sceneController;

	protected new void Start() {
		sceneController = (SceneController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager=this;
	}

	// Update is called once per frame
	protected new void FixedUpdate () {
		base.FixedUpdate ();
	}

	public void MoveToRunAction (GameObject gameObject, int newState) {
		this.RunAction (gameObject, CCMoveToAction.GetSSAction (newState), this);
	}

	#region ISSActionCallback implementation
	public void SSActionEvent (SSAction source,SSActionEventType events = SSActionEventType.Completed,
		int intParam = 0,
		string strParam = null,
		Object objectParam = null) {
		//TODO: something

	}
	#endregion
}