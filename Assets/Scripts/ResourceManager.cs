using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

	public GameObject player;
	public float dstThreshold;

	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {

		if (player != null) {
			float playerDst = Vector3.Distance (transform.position, player.transform.position);

			// Destroy the resource if it is too far away from the player
			if (playerDst > dstThreshold) {
				Object.Destroy (this.gameObject);
			}
		}

	}
}
