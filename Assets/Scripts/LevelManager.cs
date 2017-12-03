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
	private float levelStartTime;
	private float levelTimeBonusLimit;
	private int numZombiesInLevel;

    // Managers and GameObjects necessary to run this
    private GameObject scoreObject;
    private Text scoreText;
    private GameObject zombieManager;
    private Manager zombieManagerScript;
   

    // Use this for initialization
    void Start () {
        zombieManager = GameObject.Find("EnemyManager");
        if (zombieManager != null)
        {
            zombieManagerScript = zombieManager.GetComponent<Manager>();
        }

		level = 0;
		StartNewLevel ();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

	void StartNewLevel() {
		level += 1;

        // Reset Zombie Spawn Point and Increase Num Zombie Spawns
        if (zombieManagerScript != null) {
			numZombiesInLevel = 30 + (level - 1) * 20;
            zombieManagerScript.setMaxObjects(numZombiesInLevel);
        }
        Debug.Log("current level: " + this.level);

		// Get current time for a potential time kill bonus
		levelStartTime = Time.time;
		levelTimeBonusLimit = (float) numZombiesInLevel;
	}

	public void OnZombieKill() {
		// Increment the zombies killed
		numZombiesKilled += 1;

		if (numZombiesKilled >= numZombiesInLevel) {

			string levelFinishedMessage = "Completed level " + level.ToString() + ". ";

			// If zombies killed in record time, then add bonus
			float levelTimeFinished = Time.time - levelStartTime;
			if (levelTimeFinished <= levelTimeBonusLimit) {
				int bonus = numZombiesInLevel * 10;
				incrementScore (bonus);
				levelFinishedMessage += "Zombie kill time bonus: +" + bonus.ToString();
			}

			DisplayMessage(levelFinishedMessage);
		}

	}

	void OnPlayerAttacked() {
		// Decrement the player health

	}

	void DisplayMessage(string msg) {
		// TODO: Display a notification
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

		if (this.score > 1000 * this.level) {
			StartNewLevel();
		}

		if(this.score > 10000)
		{
			//you win?

		}
    }

}
