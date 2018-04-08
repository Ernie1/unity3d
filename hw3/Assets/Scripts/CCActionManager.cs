using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {

	public FirstController sceneController;

	protected new void Start() {
		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager=this;
	}

	// Update is called once per frame
	protected new void Update () {
		base.Update ();
	}

	public void singleRunAction (GameObject gameObject, Vector3 destination) {
		this.RunAction (gameObject, CCMoveToAction.GetSSAction (destination, 1), this);
	}

	public void doubleRunAction (GameObject gameObject, Vector3 via, Vector3 destination) {
		CCSequenceAction ccs = CCSequenceAction.GetSSAction (1, 0, new List<SSAction> {
			CCMoveToAction.GetSSAction (via, 1),
			CCMoveToAction.GetSSAction (destination, 1)
		});
		this.RunAction (gameObject, ccs, this);
	}

	#region ISSActionCallback implementation
	public void SSActionEvent (SSAction source,SSActionEventType events = SSActionEventType.Completed,
		int intParam = 0,
		string strParam = null,
		Object objectParam = null) {
		sceneController.someObjectHandling = false;

	}
	#endregion
}