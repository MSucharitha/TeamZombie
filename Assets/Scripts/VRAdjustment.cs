using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/******
 * This is for adjusting the game to work on the OpenVR platform used by HTC Vive and potentially Oculus Rift.
 ******/

public class VRAdjustment : MonoBehaviour {

	private Vector3 monitorRotation = new Vector3 (-12f, 0f, 0f);
	private Vector3 openVRPosition = new Vector3 (0f, -1.75f, 0f);

	void Awake () {
		// If there is a VR device at the start of the game...
		if (XRDevice.isPresent) {
			// Check if the device is using OpenVR
			if (XRSettings.loadedDeviceName == "OpenVR") {
				// ... move the camera to the floor, because that's where the tracking volume will be located
				transform.localPosition = openVRPosition;
			}
		}
	}


	void Update () {
		if (Input.GetKeyUp (KeyCode.R)) {

			// Reset the camera based on the VR Headset
			InputTracking.Recenter ();

		}

		// Reset Position if the Player is falling
		if (gameObject.transform.position.y < -0.1) {
			gameObject.transform.Translate (0f, 0.2f, 0f);
		}
	}

	void OnApplicationQuit() {
		// Disable the VR Device when the application quits. This clears any leftover VR Settings.

		XRSettings.enabled = false;

	}

}
