using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour, ISceneController, IUserAction {
	//用于scorekeeper
	public GameObject[] plane;
	//玩家
	public GameObject hero;

	public Scorekeeper scorekeeper;
	public PatrolFactory patrolFactory;
	public CCActionManager actionManager { get; set; }
//	public HeroController heroController;

	public bool gameOver;

	void Awake() {
		//请记分员
		scorekeeper = Scorekeeper.getInstance ();
		scorekeeper.sceneController = this;
		//请patrol工厂
		patrolFactory = PatrolFactory.getInstance ();
		patrolFactory.sceneController = this;
		//导演
		SSDirector director = SSDirector.getInstance();
		director.setFPS(60);
		director.currentSceneController = this;
		director.currentSceneController.LoadResources();
	}

	void Start() {
//		heroController = HeroController.getInstance ();
//		heroController.sceneController = this;

		gameOver = false;
	}

	void OnEnable() {
		//发布与订阅模式
		HeroController.scoreKeep += scorekeeper.judge;
	}

	public void LoadResources() {
		//生成巡逻兵
		GameObject patrol;
		for (int i = 0; i < plane.Length; ++i) {
			patrol = patrolFactory.getPatrol ();
			patrol.transform.parent = plane [i].transform;
			patrol.GetComponent<MoveData> ().planeNum = i;
			patrol.transform.localPosition = new Vector3 (2.2f, 0, 2.2f);
		}
	}

	void FixedUpdate() {
		
	}

	public int getScore(){
		return scorekeeper.score;
	}

	public void GameOver() {
		HeroController.scoreKeep -= scorekeeper.judge;
		hero.GetComponent<MoveData> ().planeNum = -1;
		hero.GetComponent<Animator> ().SetTrigger ("death");
		gameOver = true;
	}

	public bool isSafe(){
		return hero.GetComponent<MoveData> ().planeNum == -1;
	}

	public bool isGameOver(){
		return gameOver;
	}
}