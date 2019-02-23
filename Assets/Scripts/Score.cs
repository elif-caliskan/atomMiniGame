using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	private int score=0;
	public Text scoreText;
	private int difficultyLevel=1;
	private int maxDifficultyLevel=10;
	private int scoreToNextLevel=10;
	public bool star = false;
	public DeathMenu deathmenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		star = false;
		if (score >= scoreToNextLevel) {
			LevelUp ();
		}

		if (GetComponent<PlayerMotor> ().hiton) {
			score += difficultyLevel;
		}
		if (GetComponent<PlayerMotor> ().star) {
			Debug.Log ("star true");
			star = true;
			score += difficultyLevel*2;
			GetComponent<PlayerMotor> ().star = false;
		}
		scoreText.text = score.ToString();

	}
	void LevelUp(){
		if (difficultyLevel == maxDifficultyLevel)
			return;
		scoreToNextLevel *= 2;
		difficultyLevel++;
		GetComponent<PlayerMotor>().SetSpeed (difficultyLevel);
	}
	public void onDeath(){
		deathmenu.ToggleEndMenu (score);
	}

}
