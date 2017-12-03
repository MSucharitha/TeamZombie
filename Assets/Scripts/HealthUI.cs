using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	[SerializeField]
	GameObject fill;

	[SerializeField]
	GameObject player;

	Image fillImage;
	int currHealth;
	int maxHealth;

	// Use this for initialization
	void Start () {
		fillImage = fill.GetComponent<Image> ();
		if (fillImage == null) {
			Debug.Log ("ERROR! Fill image file not found!");
		}
		maxHealth = 10;
		currHealth = maxHealth;

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}

	// Update is called once per frame
	void Update () {

		// Rotate canvas towards the player
		transform.LookAt (player.transform.position);
		transform.Rotate (0f, 180f, 0f);

	}

	public void SetHealth(int health) {
		maxHealth = health;
		currHealth = maxHealth;
	}

	public void UpdateHealth(int health) {
		currHealth = health;

		// Guarantee that the health never goes above 1 or below 0
		float healthPercentage = Mathf.Clamp01(health / maxHealth);

		// Update the health bar game object
		fill.transform.localScale = new Vector3 (healthPercentage, 1f, 1f);

		// If below a certain threshold, change the color
		if (health <= 0.25f) {
			fillImage.color = new Color (1f, 0.3f, 0.3f); 	// red
		} else if (health <= 0.5f) {
			fillImage.color = new Color (1f, 0.75f, 0.2f); 	// orange
		} else {
			fillImage.color = new Color (0.3f, 1f, 0.3f); 	// green
		}

		// Optional: Add Health Bar Text
		// Update the health bar text
	}

	public void Show() {
		gameObject.SetActive (true);
	}
	public void Hide() {
		gameObject.SetActive (false);
	}
}
