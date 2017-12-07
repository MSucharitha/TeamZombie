using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap_VR : MonoBehaviour {

	public GameObject player;
	public float height;
	public Vector3 rotate;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (player.transform.position.x, height, player.transform.position.z);
		Vector3 playerRotation = player.transform.rotation.eulerAngles;
		playerRotation = new Vector3 (0f, playerRotation.y, 0f);
		transform.rotation = Quaternion.Euler (playerRotation);
		transform.Rotate (rotate);

	}
}
