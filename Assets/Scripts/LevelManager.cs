using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public int numZombiesKilled;
	private int score = 0;
	public int playerHealth = 10;
    public int level;
	public LevelProps[] levelProps;
   
    private GameObject scoreObject;
    private Text scoreText;

    // Managers and GameObjects necessary to run this

    // Use this for initialization
    void Start () {
		level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartNewLevel() {
		level += 1;

		// Reset Zombie Spawn Point and Increase Num Zombie Spawns

	}

	void OnZombieKill() {
		// Increment the score

	}

	void OnPlayerAttacked() {
		// Decrement the player health

	}

	[System.Serializable]
	public struct LevelProps {
		public int maxZombies;
		public int zombieKillRequirement;
		public int gunsAvailable;
	}

    public void incrementScore(int points)
    {
       
        this.score += points;
        scoreObject = GameObject.FindGameObjectWithTag("score");
        if (scoreObject != null) {
            scoreText = scoreObject.GetComponent<Text>();
        }
        scoreText.text = "Score: " + this.score;
        Debug.Log("current score: " + this.score);
        
    }

}
