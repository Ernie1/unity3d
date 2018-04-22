using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsActionManager : SSActionManager, ActionManager, ISSActionCallback {

	public DiskFactory diskFactory;

	void Start() {
		diskFactory = DiskFactory.getInstance();
	}

	public new void Update() {

	}

	public new void FixedUpdate() {
		base.FixedUpdate ();
	}

	public void singleRunAction (GameObject gameObject, int speedLevel) {
		this.RunAction (gameObject, FlyDisk.GetSSAction (speedLevel), this);
	}

	#region ISSActionCallback implementation
	public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Competeted,
		int intParam = 0, string strParam = null, Object objectParam = null){

	}
	#endregion
}