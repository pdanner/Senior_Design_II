using UnityEngine;
using System.Collections;

public class ScoreScript : Photon.MonoBehaviour {
	
	public int playerScore = 0;
	
	// Use this for initialization
	void Start () {
		GameObject.DontDestroyOnLoad (this);
	}

	public int getPlayerScore()
	{
		return playerScore;
	}

	public void setPlayerScore(int score)
	{
		playerScore = score;
	}
}

