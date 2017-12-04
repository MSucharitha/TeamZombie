using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVR : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;

	public GameObject[] guns;
	private GameObject currentGun;
	private int gunIndex = 0;

	void Awake() {

		trackedObject = GetComponent<SteamVR_TrackedObject> ();

		if (guns.Length > 0) {
			SetGun (gunIndex);
		}
	}

	void FixedUpdate () {

		device = SteamVR_Controller.Input ((int)trackedObject.index);

		// Switch weapon if grip button is pressed
		if (guns.Length > 0 && device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip)) {
			SwitchGuns ();
		}

	}

	void SetGun(int index) {

		Vector3 newTranslate = Vector3.zero;
		Vector3 newScale = Vector3.one * 0.01f;
		Vector3 newRotate = new Vector3 (30f, 0f, 0f);

		// Destroy the current Gun to make room for the next Gun
		if (currentGun != null) {
			newTranslate = currentGun.transform.position;
			newScale = currentGun.transform.localScale;
			newRotate = currentGun.transform.rotation.eulerAngles;

			Destroy (currentGun);
		}

		// Create new Gun
		currentGun = Instantiate (guns [index]);

		// Set new Gun to be parented by the current Gameobject
		currentGun.transform.SetParent(gameObject.transform);

		// Reset transforms
		currentGun.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
		currentGun.transform.Translate (newTranslate);
		// currentGun.transform.Translate (Vector3.zero, gameObject.transform);
		currentGun.transform.Rotate(newRotate);
		// currentGun.transform.Rotate (30f, 0f, 0f, Space.Self);


	}

	void SwitchGuns() {

		gunIndex = (gunIndex + 1) % guns.Length;
		SetGun (gunIndex);

	}
}
