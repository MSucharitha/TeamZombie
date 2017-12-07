using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHealthVR : MonoBehaviour {

	public int gameOverSceneIndex = 2;
	public int health;
	public int maxHealth = 100;
	public GameObject HealthFillPrefab;
	private GameObject HealthFill;
	private UnityEngine.UI.Text textToUpdate;
	private UnityEngine.UI.Image HealthFillImage;
	private FPSHealth healthManager;

	// Use this for initialization
	void Start () {
		healthManager = GetComponent<FPSHealth> ();
		if (healthManager != null) {
			SetHealth (healthManager.maxHealth);
		} else {
			SetHealth (100);
		}
	}

	void Update() {
		// Check Player Health and Update Health Accordingly
//		ApplyHealthDamage(1);
		if (healthManager.health != health) {
			UpdateHealth (healthManager.health);
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
		textToUpdate = HealthFillPrefab.transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>();
		textToUpdate.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
		UpdateHealthBar ();
	}

//	public void ApplyHealthDamage(int damage) {
//		// Reduce Health
//		health = (health - damage + maxHealth) % maxHealth;
//
//		// Update UI elements: Text and Health Bar Fill Size
//		textToUpdate = HealthFillPrefab.transform.GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Text>();
//		textToUpdate.text = "Health: " + health.ToString() + "/" + maxHealth.ToString();
//		UpdateHealthBar ();
//	}

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
