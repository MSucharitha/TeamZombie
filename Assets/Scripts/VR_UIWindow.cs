using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_UIWindow : MonoBehaviour {

	public GameObject VR_UIPanel;
	public GameObject VR_UICanvas;
	public UnityEngine.UI.Text VR_UIText;
	public Color panelColor;
	public Color textColor;
	public GameObject player;
	public float heightOffset = 0;
	public bool RotateYForLookAtFunction = false;

	private Material material;

	void Start() {
		if (panelColor == null) {
			panelColor = Color.gray;
		}
		if (textColor == null) {
			textColor = Color.black;
		}

		if (VR_UIPanel == null) {
			VR_UIPanel = transform.GetChild (0).gameObject;
		}
		if (VR_UICanvas == null) {
			VR_UICanvas = transform.GetChild (1).gameObject;
		}
		if (VR_UIText == null) {
			VR_UIText = VR_UICanvas.transform.GetChild (0).GetComponent<UnityEngine.UI.Text> ();
		}
		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		ChangePanelColor (panelColor);
//		ChangeTextColor (textColor);
	}

	public void Update() {
		// Move to face the player
		Vector3 newPosition = player.transform.position + player.transform.forward * 1f; 
		newPosition.Set (newPosition.x, newPosition.y + 5.1f + heightOffset, newPosition.z);
		transform.position = newPosition;

		Vector3 lookAtPosition = player.transform.position;
		float rotateY = newPosition.y;
		if (RotateYForLookAtFunction) {
			rotateY = rotateY - 1f;
		}
		lookAtPosition.Set (lookAtPosition.x, rotateY, lookAtPosition.z);
		transform.LookAt (lookAtPosition);
		transform.Rotate (0f, 180f, 0f);

	}

	public void UpdateText(int index, string newMessage) {
		
	}

	public void ChangePanelColor(Color newColor) {
		panelColor = newColor;

		// Change the color of the panel's cube gameobject
		material = new Material (Shader.Find ("Diffuse"));
		material.color = panelColor;
		VR_UIPanel.GetComponent<Renderer> ().material = material;
	}

	public void ChangeTextColor(Color color) {
		textColor = color;

		// Change color of the VR_UIText element
		VR_UIText.color = textColor;
	}

	public void ChangeText(string message) {
		VR_UIText.text = message;
	}
}
