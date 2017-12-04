using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class VR_Gun : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device device;

	public Gun[] guns;
	private GameObject currentGun;
	private int gunIndex = 0;

	public ParticleSystem muzzleFlash;
	public Camera fpsCam;

	Animation gunAnimator;

	float lastShoot;

	// Following invisible objects are to determine direction
	public GameObject startSphere;
	public GameObject endSphere;
//	public GameObject bullet;
	public bool seeSpheres = false;

	void Awake() {

		trackedObject = GetComponent<SteamVR_TrackedObject> ();

		if (guns.Length > 0) {
			SetGun (gunIndex);
		}

		lastShoot = Time.time;

		startSphere.GetComponent<MeshRenderer> ().enabled = seeSpheres;
		endSphere.GetComponent<MeshRenderer> ().enabled = seeSpheres;
	}

	void FixedUpdate () {

		device = SteamVR_Controller.Input ((int)trackedObject.index);

		// Switch weapon if grip button or touchpad is pressed
		if (guns.Length > 0){
			if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Grip)) {
				// Grip pressed, move right
				SwitchGuns (1);
			} else if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && TouchpadPressedRight()) {
				// Right touchpad pressed, move right
				SwitchGuns (1);
			} else if (device.GetPressDown (SteamVR_Controller.ButtonMask.Touchpad) && TouchpadPressedLeft ()) {
				// Left touchpad pressed, move left
				SwitchGuns (-1);
			}
		}

	}

	void Update() {

		// Determine the direction of the gun (not exactly the direction of the controller)
		startSphere.transform.position = transform.position;
		endSphere.transform.position = transform.position + transform.forward * 5f;
		endSphere.transform.RotateAround (transform.position, transform.right, 30f);

		// Shoot every 0.5s if the trigger is held down
		if (device.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
			float currTime = Time.time;
			if (currTime - lastShoot > 0.5f) {
				Shoot ();
				lastShoot = currTime;
			}
		}
	}

	bool TouchpadPressedLeft() {
		Vector2 touchpad = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
		return touchpad.x < -0f;
	}
	bool TouchpadPressedRight() {
		Vector2 touchpad = device.GetAxis (Valve.VR.EVRButtonId.k_EButton_Axis0);
		return touchpad.x > 0f;
	}

	void SetGun(int index) {
		
		Vector3 newScale = Vector3.one * 0.01f;
		Vector3 newRotate = new Vector3 (30f, 0f, 0f);

		// Destroy the current Gun to make room for the next Gun
		if (currentGun != null) {
			newScale = currentGun.transform.localScale;
			newRotate = currentGun.transform.rotation.eulerAngles;

			Destroy (currentGun);
		}

		// Create new Gun
		currentGun = Instantiate (guns[index].gunObject);

		// Set new Gun to be parented by the current Gameobject
		currentGun.transform.SetParent(gameObject.transform);

		// Reset transforms
		currentGun.transform.localScale = guns[index].scale;
		currentGun.transform.localPosition = Vector3.zero;
		currentGun.transform.Rotate(newRotate);

		// Set MuzzleFlash location for each gun
		muzzleFlash.transform.localPosition = guns[index].muzzleLocation;

		// Stop animation
		gunAnimator = currentGun.GetComponent<Animation>();
		gunAnimator.playAutomatically = false;

	}

	void SwitchGuns(int mode) {
		// Switch gun to the next in line
		if (mode == 1) {
			gunIndex = (gunIndex + 1) % guns.Length;
		} else if (mode == -1) {
			gunIndex = (gunIndex - 1 + guns.Length) % guns.Length;
		}

		SetGun (gunIndex);
	}

	void Shoot() {
		// Play gun flash
		muzzleFlash.Play();
		gunAnimator.Play ("Shoot");

		// Check if there's a collision
		RaycastHit hit;
		Vector3 rayOrigin = transform.position;
		Vector3 rayDirection = endSphere.transform.position - transform.position;

		// bool foundCollision = Physics.Raycast (transform.position, transform.forward, out hit, guns[gunIndex].maxDistance);
		bool foundCollision = Physics.Raycast (rayOrigin, rayDirection, out hit, guns[gunIndex].maxDistance);

		if (foundCollision) {
			GameObject hitObj = hit.collider.gameObject;
			Debug.Log ("Shot object " + hitObj.transform.name);

			// If zombie is hit, call shooting function for the zombie
			if (hitObj.transform.tag == "zombie") {
				AnimController2 zombieCtrl = hitObj.GetComponent<AnimController2> ();
				zombieCtrl.shoot (guns [gunIndex].damage);
			}
		} else {
			Debug.Log ("Nothing shot");
		}

	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == transform.tag || collision.gameObject.tag == "Untagged") {
			Physics.IgnoreCollision (collision.collider, GetComponent<Collider>());
		}
	}

	[System.Serializable]
	public struct Gun {
		public GameObject gunObject;
		public Vector3 scale;
		public Vector3 muzzleLocation;
		public int damage;
		public float maxDistance;
	}
}
