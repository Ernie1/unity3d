using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback {

	public FirstController sceneController;
//	public CCMoveToAction moveToA, moveToB, moveToC, moveToD;
//	public CCMoveToAction boatToHere, boatToThere, moveToHereSharp, moveToThereSharp, moveToHereSeat0, moveToHereSeat1, moveToThereSeat0, moveToThereSeat1;

	protected new void Start() {
		sceneController = (FirstController)SSDirector.getInstance ().currentSceneController;
		sceneController.actionManager=this;
//		boatToHere = CCMoveToAction.GetSSAction (new Vector3 (2, 0.1F, 0), 1);
//		boatToThere = CCMoveToAction.GetSSAction (new Vector3 (-2, 0.1F, 0), 1);
//		moveToHereSharp = CCMoveToAction.GetSSAction (new Vector3 (3, 1, 0), 1);
//		moveToThereSharp = CCMoveToAction.GetSSAction (new Vector3 (3, -1, 0), 1);
//		moveToHereSeat0 = CCMoveToAction.GetSSAction (new Vector3 (1.7F, 0.5F, 0), 1);
//		moveToHereSeat1 = CCMoveToAction.GetSSAction (new Vector3 (2.3F, 0.5F, 0), 1);
//		moveToThereSeat0 = CCMoveToAction.GetSSAction (new Vector3 (-2.3F, 0.5F, 0), 1);
//		moveToThereSeat1 = CCMoveToAction.GetSSAction (new Vector3 (-1.7F, 0.5F, 0), 1);

//		moveToA = CCMoveToAction.GetSSAction (new Vector3 (5, 0, 0), 1);
//		this.RunAction (sceneController.move1, moveToA, this);
//		moveToC = CCMoveToAction.GetSSAction (new Vector3 (-2, -2, -2), 1);
//		moveToD = CCMoveToAction.GetSSAction (new Vector3 (3, 3, 3), 1);
//		CCSequenceAction ccs = CCSequenceAction.GetSSAction (3, 0, new List<SSAction> { moveToC, moveToD });
//		this.RunAction (sceneController.move2, ccs, this);
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
//		if (source == moveToA) {
//			moveToB = CCMoveToAction.GetSSAction (new Vector3 (-5, 0, 0), 1);
//			this.RunAction (sceneController.move1, moveToB, this);
//		} else if (source == moveToB) {
//			moveToA = CCMoveToAction.GetSSAction (new Vector3 (5, 0, 0), 1);
//			this.RunAction (sceneController.move1, moveToA, this);
//		}
	}
	#endregion
}