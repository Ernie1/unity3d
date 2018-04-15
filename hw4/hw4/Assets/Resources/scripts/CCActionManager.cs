using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {

	public SceneController sceneController;
	public DiskFactory diskFactory;

	void Start() {
		sceneController = (SceneController)SSDirector.getInstance ().currentScenceController;
		sceneController.actionManager=this;
		diskFactory = DiskFactory.getInstance();
	}

	public new void Update () {
		base.Update ();
	}

	public void singleRunAction (GameObject gameObject, int speedLevel) {
		this.RunAction (gameObject, Emit2.GetSSAction (speedLevel), this);
	}

	#region ISSActionCallback implementation
	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null){

	}
	#endregion
}