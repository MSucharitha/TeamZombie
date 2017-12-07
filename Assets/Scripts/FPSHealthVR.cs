using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHealthVR : MonoBehaviour {

	public int gameOverSceneIndex = 2;
	public int health;
	public int maxHealth = 100;
	public int score;
	public GameObject HealthFillPrefab;
	public GameObject ScorePrefab;
	public LevelManager levelManager;

	private GameObject HealthFill;
	private UnityEngine.UI.Text healthTextToUpdate;
	private UnityEngine.UI.Image HealthFillImage;
	private FPSHealth healthManager;

	private UnityEngine.UI.Text scoreTextToUpdate;

	// Use this for initialization
	void Start () {
		healthManager = GetComponent<FPSHealth> ();
		if (healthManager != null) {
			SetHealth (healthManager.maxHealth);
		} else {  
			SetHealth (100);
		}
		if (levelManager != null) {
			score = levelManager.score;
		}
	}

	void Update() {
		// Check Player Health and Update Health Accordingly
		if (healthManager.health != health) {
			UpdateHealth (healthManager.health);
		}
		if (score != levelManager.score) {
			UpdateScore (levelManager.score);
		}

	}

	void SetHealth(int maxHealthValue) {
		maxHealth = maxHealthValue;
		health = maxHealth;
	}

	public void UpdateHealth(int newHealth) {
		// Update health
		health = newHealth;

		// Update UI elements: Text and Health Bar Fill Size
		healthTextToUpdate = HealthFillPrefab.transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>();
		healthTextToUpdate.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
		UpdateHealthBar ();
	}

	public void UpdateScore(int newScore) {
		// Update score
		score = newScore;

		// Update UI elements: text
		scoreTextToUpdate = ScorePrefab.transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>();
		scoreTextToUpdate.text = "Score: " + padScore(score);
	}

	private string padScore(int score) {
		string scoreString = score.ToString ();
		return (new string('0', 6 - scoreString.Length) + scoreString);
	}

	public void UpdateHealthBar() {
		// Get HealthFill Image
		HealthFill = HealthFillPrefab.transform.GetChild(1).GetChild(1).gameObject;
		HealthFillImage = HealthFill.GetComponent<UnityEngine.UI.Image> ();

		// Calculate Percentage and Scale the health bar accordingly
		float healthPercentage = Mathf.Clamp01 ((float)health / (float)maxHealth);
		HealthFillImage.transform.localScale = new Vector3 (healthPercentage, 1f, 1f);

		// Change the health bar color based on health percentage
		if (HealthFillImage != null) {
			if (healthPercentage <= 0.25f) {
				HealthFillImage.color = new Color (1f, 0.3f, 0.3f); 	// red
			} else if (healthPercentage <= 0.5f) {
				HealthFillImage.color = new Color (1f, 0.75f, 0.2f); 	// orange
			} else {
				HealthFillImage.color = new Color (0.3f, 1f, 0.3f); 	// green
			}
		}
	}
}
