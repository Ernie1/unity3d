using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {

	public CCActionManager actionManager;

	private GameObject objectClicked;

    bool[] boatSeat;

	public bool someObjectHandling;
    private bool pausing;
    //private int handleNum;
    private int sailTo;

    private SSDirector director;
    private UserGUI userGUI;
    private Model model;

    void Awake () {
        model = Model.getInstance ();

        userGUI = gameObject.AddComponent<UserGUI> ();
        userGUI.action = this;

        director = SSDirector.getInstance ();
        director.currentSceneController = this;
        director.setFPS (60);
        director.currentSceneController.LoadResources ();
    }

	void Start () {
		actionManager = gameObject.AddComponent<CCActionManager> ();
	}

    // 对ISceneController的实现
    public void LoadResources () {

        boatSeat = new bool[]{ true, true };
        someObjectHandling = false;

        model.GenGameObjects();

        pausing = false;
        userGUI.status = 2;
    }

    public void Pause () {
        pausing = true;
        userGUI.status = 3;
    }

    public void Resume () {
        pausing = false;
        userGUI.status = 2;
    }

    public void Restart () {
        model.destroy ();
        LoadResources ();
    }

    public void GameOver () {
        pausing = true;
    }

    void handleClickedObject () {
		
        
		//handleNum = -1;
        //boat
        if (model.isBoat (ref objectClicked)) {
            if (someoneOnBoat ()) {             
				actionManager.singleRunAction (objectClicked, model.boatPos [1 - model.whereBoat ()]);
            } else
                someObjectHandling = false;
            return;
        }
        //model.setJumpFromObject (ref objectClicked);
        //priest
        int index = model.whichPriest(ref objectClicked);
        if (index != -1) {
            if (model.wherePriest (index) == 2) {
				actionManager.doubleRunAction (objectClicked, model.bankSharp [model.whereBoat ()], model.priestPos [index, model.whereBoat ()]);
				boatSeat [model.whichSeatByPosition (ref objectClicked)] = true;
				model.ashore (ref objectClicked);
            } else if (model.wherePriest (index) == model.whereBoat () && whereCanSit () != 2) {
				actionManager.doubleRunAction (objectClicked, model.bankSharp [model.whereBoat ()], model.seatGlobalPos (whereCanSit ()));
				boatSeat [whereCanSit ()] = false;
				model.aboard (ref objectClicked);
            } else
                someObjectHandling = false;
            return;
        }
        //devil
        index = model.whichDevil(ref objectClicked);
        if (index != -1) {
            if (model.whereDevil (index) == 2) {
				actionManager.doubleRunAction (objectClicked, model.bankSharp [model.whereBoat ()], model.devilPos [index, model.whereBoat ()]);
				boatSeat [model.whichSeatByPosition (ref objectClicked)] = true;
				model.ashore (ref objectClicked);
            }
            else if (model.whereDevil (index) == model.whereBoat () && whereCanSit () != 2) {
				actionManager.doubleRunAction (objectClicked, model.bankSharp [model.whereBoat ()], model.seatGlobalPos (whereCanSit ()));
				boatSeat [whereCanSit ()] = false;
				model.aboard (ref objectClicked);
            } else
                someObjectHandling = false;
            return;
        }
        //其它不管
        someObjectHandling = false;
    }


    int whereCanSit() {
        if (boatSeat [0])
            return 0;
        if (boatSeat [1])
            return 1;
        return 2;
    }

    bool someoneOnBoat () {
        if(boatSeat[0]&&boatSeat[1])
            return false;
        return true;
    }

    //0 lose 1 win 2 -
    int checkGame () {
        int priestHere = 0, priestThere = 0, devilHere = 0, devilThere = 0;
        for(int i=0;i<3;++i){
            if (model.wherePriest (i) == 0)
                ++priestHere;
            else if (model.wherePriest (i) == 1)
                ++priestThere;
            else if (model.whereBoat () != 2) {
                if (model.whereBoat () == 0)
                    ++priestHere;
                else
                    ++priestThere;
            }
            if (model.whereDevil (i) == 0)
                ++devilHere;
            else if (model.whereDevil (i) == 1)
                ++devilThere;
            else if (model.whereBoat () != 2) {
                if (model.whereBoat () == 0)
                    ++devilHere;
                else
                    ++devilThere;
            }
        }
        if ((priestHere > 0 && priestHere < devilHere) || (priestThere > 0 && priestThere < devilThere))
            return 0;
        if (priestThere == 3 && devilThere == 3)
            return 1;
        return 2;
    }

    // Update is called once per frame
    void Update () {
        if (!pausing) {
            if (!someObjectHandling && userGUI.action.checkObjectClicked ())
                handleClickedObject ();
//            if (someObjectHandling) {
//                if (handleNum == 0)
//                    someObjectHandling = model.sailing (sailTo);
//                if (handleNum == 1) {
//                    someObjectHandling = model.jumping (ref objectClicked, 0);
//                }
//                if (handleNum == 2) {
//                    someObjectHandling = model.jumping (ref objectClicked, 1);
//                }
//            }
            userGUI.status = checkGame ();
        }
    }

    void IUserAction.GameOver () {
		director.currentSceneController.GameOver ();
    }

    void IUserAction.Pause () {
		director.currentSceneController.Pause ();
    }

    void IUserAction.Resume () {
		director.currentSceneController.Resume ();
    }

    void IUserAction.Restart () {
		director.currentSceneController.Restart ();
    }

    bool IUserAction.checkObjectClicked(){
        if(Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit ;
            if(Physics.Raycast (ray,out hit)) {
                objectClicked = hit.collider.gameObject;
                someObjectHandling = true;
                return true;
            }
        }
        return false;
    }
}


//////////////
//public void singleRunAction (GameObject gameObject, Vector3 destination) {
//	this.RunAction (gameObject, CCMoveToAction.GetSSAction (destination), this);
//}
//
//public void doubleRunAction (GameObject gameObject, Vector3 via, Vector3 destination) {
//	CCSequenceAction ccs = CCSequenceAction.GetSSAction (1, 0, new List<SSAction> {
//		CCMoveToAction.GetSSAction (via, 1),
//		CCMoveToAction.GetSSAction (destination, 1)
//	});
//	this.RunAction (gameObject, ccs, this);
//}