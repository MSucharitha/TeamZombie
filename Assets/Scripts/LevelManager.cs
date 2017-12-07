using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public int numZombiesKilled;
	public int score = 0;
	public int playerHealth = 10;
    public int level;
	public LevelProps[] levelProps;
	private float levelStartTime;
	private float levelTimeBonusLimit;
	private int totalZombiesKilled;
	private int maxNumZombiesInLevel;
    private GameObject player;
    private FPSHealth fpsHealth;
    // Managers and GameObjects necessary to run this
    private GameObject scoreObject;
    private Text scoreText;
    private GameObject zombieManager;
    private Manager zombieManagerScript;

	[System.Serializable]
	public enum TargetPlatform {GoogleCardboard, HTCVive};
	public TargetPlatform targetPlatform;
	public GameObject Gun;

	public GameObject notificationSystem;
	private UnityEngine.UI.Text notificationText;
	private float lastNotificationTime = -1000f;
	public float notificationActiveTime = 5f;


    // Use this for initialization
    void Start() {
        zombieManager = GameObject.Find("EnemyManager");
        if (zombieManager != null)
        {
            zombieManagerScript = zombieManager.GetComponent<Manager>();
        }

        player = GameObject.FindWithTag("Player");
        if (player != null) {
            fpsHealth = player.GetComponent<FPSHealth>();
        }

        //to test levels
        level = 0;
		totalZombiesKilled = 0;
		StartNewLevel ();


		if (notificationSystem != null) {
			notificationSystem.SetActive (false);
			notificationText = notificationSystem.transform.GetChild (1).GetChild (0).GetComponent<UnityEngine.UI.Text> ();
		}
    }
	
	// Update is called once per frame
	void Update () {

		// Deactivate notification system if it's been here for at least 5s or however long
		if (notificationSystem != null && notificationSystem.activeSelf) {
			float currTime = Time.time;
			if (currTime - lastNotificationTime > notificationActiveTime) {
				notificationSystem.SetActive (false);
			}
		}
	}

	void StartNewLevel() {
		level += 1;
		totalZombiesKilled += numZombiesKilled;
		numZombiesKilled = 0;

		string message = "Reached Level " + level + "!";

        //add bonus health points for player
        if (fpsHealth != null) {
            fpsHealth.healthIncreaseBonus(20);
        }
        // Reset Zombie Spawn Point and Increase Num Zombie Spawns
        if (zombieManagerScript != null) {
			maxNumZombiesInLevel = 20 + (level - 1) * 10;
            zombieManagerScript.setMaxObjects(maxNumZombiesInLevel);
            //decrease spawntime
            float spawnTimeForCurrentLevel = Mathf.Max(4 - 0.5f * level, 0.2f) ;
            zombieManagerScript.setSpawnTime(spawnTimeForCurrentLevel);
            //add new types
            if (level > 1)
            {
                zombieManagerScript.addZombieTypes(level);
            }
        }
        Debug.Log("current level: " + this.level);

		// Get current time for a potential time kill bonus
		levelStartTime = Time.time;
		levelTimeBonusLimit = (float) maxNumZombiesInLevel;

		// Switch Guns or Increase Gun Availability
		// Levels to switch: [3, 5]
		if (targetPlatform == TargetPlatform.GoogleCardboard) {
			if (level == 3 || level == 5) {
				message += " Gained new weapon!";
				Gun.GetComponent<weaponSwitching>().selectWeapon();
			}
		} else if (targetPlatform == TargetPlatform.HTCVive) {
			if (level == 3) {
				message += " Gained new weapon!";
				Gun.GetComponent<VR_Gun> ().IncreaseGunAvailability (2);
			} else if (level == 5) {
				message += " Gained new weapon!";
				Gun.GetComponent<VR_Gun> ().IncreaseGunAvailability (3);
			}
		}

		// Apply notification
		if (notificationSystem != null && level > 1) {
			notificationSystem.SetActive (true);
			lastNotificationTime = Time.time;

			notificationText = notificationSystem.transform.GetChild (1).GetChild (0).GetComponent<UnityEngine.UI.Text> ();
			notificationText.text = message;
		}
	}

	public void OnZombieKill() {
		// Increment the zombies killed
		numZombiesKilled += 1;

		if (numZombiesKilled >= maxNumZombiesInLevel) {

			string levelFinishedMessage = "Completed level " + level.ToString() + ". ";

			// If zombies killed in record time, then add bonus
			float levelTimeFinished = Time.time - levelStartTime;
			if (levelTimeFinished <= levelTimeBonusLimit) {
				int bonus = maxNumZombiesInLevel * 10;
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

    public void incrementScore(int points)
    {
       
        this.score += points;
        scoreObject = GameObject.FindGameObjectWithTag("score");
        if (scoreObject != null) {
            scoreText = scoreObject.GetComponent<Text>();
        }
		if (scoreObject != null) {
			scoreText.text = "Score: " + this.score;
		}

		if (this.score > 1000 * this.level) {
			StartNewLevel();
		}

		if(this.score > 10000)
		{
			//you win?

		}
    }
}

[System.Serializable]
public struct LevelProps {
//	public int maxZombies;
//	public int zombieKillRequirement;
	public int gunsAvailable;
}