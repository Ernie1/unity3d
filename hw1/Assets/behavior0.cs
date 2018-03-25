using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behavior0 : MonoBehaviour {

	public int scale;

	int[,] matrix = new int [10, 10];
	int height = 50, width = 50;
	int firstX, firstY;

	private Texture gouImg;
	private Texture chaImg;

	private int turn;

	private GUIStyle style = new GUIStyle ();

	void Awake () {
		gouImg = Resources.Load ("勾") as Texture;
		chaImg = Resources.Load ("叉") as Texture;
		style.normal.textColor = Color.white;
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 20;
	}


	void Start () {
		Reset ();
	}

	void Reset () {
		for(int i=0;i<scale;++i)
			for(int j=0;j<scale;++j)
				matrix[i,j]=0;
		turn = 1;
		firstX = Screen.width / 2 - scale * width / 2;
		firstY = Screen.height / 2 - scale * height / 2;
	}
	string count = "3";
	void OnGUI () {
		GUI.skin.button.fontSize = 20;
		for (int i = 0; i < scale; ++i)
			for (int j = 0; j < scale; ++j) {
				if (matrix [i, j] == 0)
				if (GUI.Button (new Rect (firstX + width * j, firstY + height * i, width, height), "") && win() == 0) {
						matrix [i, j] = turn;
						turn = 3 - turn;
					}
				if (matrix [i, j] == 1)
					GUI.Button (new Rect (firstX + width * j, firstY + height * i, width, height), gouImg);
				if (matrix [i, j] == 2)
					GUI.Button (new Rect (firstX + width * j, firstY + height * i, width, height), chaImg);
			}
		if (modi ())
		if (GUI.Button (new Rect (firstX, firstY + scale * height + 30, width * scale-2, height), "Reset"))
				Reset ();
		if (win () == 1)
			GUI.Label (new Rect (firstX, firstY - height - 10, width * scale, height), "勾赢了", style);
		else if(win() == 2)
			GUI.Label (new Rect (firstX, firstY - height - 10, width * scale, height), "叉赢了", style);

		count = GUI.TextField(new Rect (firstX+width * scale/2-10, firstY + scale * height +5, 20, 20),count,1);
		int o;
		if (count != scale.ToString () && int.TryParse (count, out o) && o > 1) {
			scale = o;
			Reset ();
		} else if(count!="")
			count = scale.ToString ();

	}

	bool modi () {
		foreach (int i in matrix)
			if (i!=0)
				return true;
		return false;
	}

	int win () {
		int first;
		//横
		for (int i=0; i<scale; ++i) {
			first=matrix[i, 0];
			if (first != 0)
				for (int j = 1; j < scale; ++j) {
					if (matrix [i, j] != first)
						break;
					if (j == scale - 1)
						return first;
				}
		}
		//纵
		for (int j=0; j<scale; ++j) {
			first=matrix[0, j];
			if (first != 0)
				for (int i = 1; i < scale; ++i) {
					if (matrix [i, j] != first)
						break;
					if (i == scale - 1)
						return first;
				}
		}
		//
		first = matrix[0, 0];
		if(first!=0)
			for (int i = 1; i < scale; ++i) {
				if (matrix [i, i] != first)
				break;
				if (i == scale - 1)
					return first;
			}
		//
		first = matrix[0, scale-1];
		if(first!=0)
			for (int i = 1; i < scale; ++i) {
				if (matrix [i, scale - 1 - i] != first)
					break;
				if (i == scale - 1)
					return first;
			}
		return 0;
	}
}
