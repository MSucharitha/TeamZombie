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

    // Managers and GameObjects necessary to run this
    private GameObject scoreObject;
    private Text scoreText;
    private GameObject zombieManager;
    private Manager zombieManagerScript;
   

    // Use this for initialization
    void Start () {
		level = 1;
        zombieManager = GameObject.Find("EnemyManager");
        if (zombieManager != null)
        {
            zombieManagerScript = zombieManager.GetComponent<Manager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (this.score > 1000 * this.level) {
            StartNewLevel();
        }

        if(this.score > 10000)
        {
            //you win?

        }
	}

	void StartNewLevel() {
		level += 1;

        // Reset Zombie Spawn Point and Increase Num Zombie Spawns
        if (zombieManagerScript != null) {
            zombieManagerScript.setMaxObjects(30 + (level-1) * 20);
        }
        Debug.Log("current level: " + this.level);
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
       
    }

}
