using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour {
	public SceneController sceneController;
	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
		sceneController = (SceneController)SSDirector.getInstance ().currentSceneController;
	}

	void FixedUpdate() {
		if (sceneController.hero.GetComponent<MoveData> ().planeNum == GetComponent<MoveData> ().planeNum && !sceneController.gameOver) {
			sceneController.actionManager.CatchRunAction (gameObject, sceneController.hero);
		} else {
			if (!shouldChangeState ())
				sceneController.actionManager.MoveRunAction (gameObject, gameObject.GetComponent<MoveData> ().state);
			else {
				//逆时针
				sceneController.actionManager.MoveRunAction (gameObject, (gameObject.GetComponent<MoveData> ().state + 3) % 4);
			}
		}
		animator.SetBool ("run", true);
	}

	bool shouldChangeState() {
		if (gameObject.transform.localPosition.x >= 4) {
			gameObject.transform.localPosition = new Vector3 (3.9f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
			return true;
		}
		else if (gameObject.transform.localPosition.x <= -4) {
			gameObject.transform.localPosition = new Vector3 (-3.9f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
			return true;
		}
		else if (gameObject.transform.localPosition.z >= 4) {
			gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 3.9f);
			return true;
		}
		else if (gameObject.transform.localPosition.z <= -4) {
			gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -3.9f);
			return true;
		}
		return false;
	}

	void OnCollisionStay(Collision e) {
		if (e.gameObject.Equals (sceneController.hero)) {
			sceneController.GameOver ();
		}
	}
}