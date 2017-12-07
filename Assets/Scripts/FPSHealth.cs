using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSHealth : MonoBehaviour {
    [SerializeField] public int index;
    public ApplicationModel app;
    private Text healthText;
	private Text scoreText;
	public int maxHealth = 100;
	public int health = 100;
	private float lastDamageTime = -100f;
	private float damageEveryXSeconds = 0.5f;

	public void Start() {
		health = maxHealth;
	}

    public int gameOverSceneIndex = 2;
    public void healthDamaged(int damage) {
		// Calculate the new health
		health = Mathf.Clamp(health - damage, 0, maxHealth);

		// Update the health text
		if (healthText == null) {
			GameObject healthObject = GameObject.FindGameObjectWithTag ("health");
			if (healthObject != null) {
				healthText = healthObject.GetComponent<Text>();
			}
		}
		if (healthText != null) { // might not exist (as in HTC Vive)
			healthText.text = "Health : " + health + "%";
		}

		// If the health is 0, kill the player
		if (health <= 0) {
			die ();
		}
    }


    public void healthIncreaseBonus(int bonus) {

		// Get the healthText gameobject (might be removed over time)
		if (healthText == null) {
            healthText = GameObject.FindGameObjectWithTag("health").GetComponent<Text>();
        }

		// Update the health
		if (health < maxHealth) {
			health = Mathf.Min (maxHealth, health + bonus);

			if (healthText != null) { // Might be null (as in for the HTC Vive)
				healthText.text = "Health : " + health + "%";
			}
		}
    }

    public void die() {
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        int score = 0;
        System.Int32.TryParse(scoreText.text.Split(':')[1], out score);
        ApplicationModel.score = score;
        SceneManager.LoadScene(gameOverSceneIndex);
    }

    void OnTriggerEnter(Collider col) {
		// Apply damage every 0.5 seconds if collided into a zombie
		if (col.gameObject.tag == "zombie") {
			float currTime = Time.time;
			if (currTime - lastDamageTime > damageEveryXSeconds) {
				lastDamageTime = currTime;
				healthDamaged (10);
			}
        }
    }

    void OnTriggerStay(Collider col) {
		// Apply damage every 0.5 seconds if collided into a zombie
		if (col.gameObject.tag == "zombie") {
			float currTime = Time.time;
			if (currTime - lastDamageTime > damageEveryXSeconds) {
				lastDamageTime = currTime;
				healthDamaged (10);
			}

        }
    }

}
