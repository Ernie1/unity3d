using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour, ISceneController, IUserAction
{
    public CCActionManager actionManager { get; set; }
	public Scorekeeper scorekeeper;
	public DiskFactory DF;
    private int round = 0;
	public int totalRound = 3;
	public int trial = 10;
    public Text ScoreText;
    public Text RoundText;
    public Text GameText;
    private bool play = false;
    private int num = 0;
	private float heartbeat;

    GameObject disk;
    GameObject explosion;
    
    void Awake() {
        SSDirector director = SSDirector.getInstance();
        DF = DiskFactory.getInstance();
        DF.sceneControler = this;
        director.setFPS(60);
        director.currentScenceController = this;
        director.currentScenceController.LoadResources();

		scorekeeper = Scorekeeper.getInstance ();
    }
    void Start() {
        round = 1;
		heartbeat = 0;
    }
    public void LoadResources() {
        explosion = Instantiate(Resources.Load("prefabs/Explosion"), new Vector3(-40, 0, 0), Quaternion.identity) as GameObject;
        Instantiate(Resources.Load("prefabs/Light"));
    }

    void Update() {
		if (play) {
			if (heartbeat >= 1) {
				launchDisk ();
				heartbeat = 0;
			}
			heartbeat += Time.deltaTime;
		}

		if (Input.GetButtonDown("Fire1") && play) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
            	GameText.text = "";
                if (hit.transform.tag == "Disk")
                {
                    explosion.transform.position = hit.collider.gameObject.transform.position;
                    explosion.GetComponent<ParticleSystem>().Play();
                    hit.collider.gameObject.SetActive(false);
					scorekeeper.record (hit.collider.gameObject);
                }
            }
        }
		updateStatus ();
    }

    public void StartGame() {
        num = 0;
		play = true;
		scorekeeper.reset ();
    }
    public void ReStart() {
		round = 1;
		heartbeat = 0;
		num = 0;
		play = true;
		GameText.text = "";
		DF.hideAll ();
		scorekeeper.reset ();
    }

	private void launchDisk() {
		GameObject newDisk = DF.getDisk ();
		if (!newDisk.GetComponent<DiskData> ())
			newDisk.AddComponent<DiskData> ();
		newDisk.GetComponent<DiskData> ().score = round;
		newDisk.GetComponent<Renderer> ().material.color = new Color ((round % 10) * 0.1F, 0.4F, 0.8F);


		float size = (float)(1.0f - 0.2 * round);
		newDisk.transform.localScale = new Vector3(4*size, size/5, 4*size);
		num++;
		actionManager.singleRunAction (newDisk, round);
	}

	private void updateStatus() {
		ScoreText.text = "Score:" + scorekeeper.score.ToString();
		RoundText.text = "Round:" + round.ToString();
		if (scorekeeper.score >= 6) {
			++round;
			GameText.text = "Round " + round.ToString();
			scorekeeper.reset ();
			num = 0;
		}
		if (round > totalRound) {
			play = false;
			GameText.text = "Win";
		}
		if (num >= trial) {
			play = false;
			GameText.text = "Game Over";
		}
	}
}