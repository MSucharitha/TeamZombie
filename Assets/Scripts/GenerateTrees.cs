using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrees : MonoBehaviour {

	public GameObject player;
	public EnvResource[] naturalRsrc;
	public int numRsrcAroundPlayer;
	public float radialMax;
	public int RNGSeed;

	System.Random rsrcRandomizer;

	void Start () {
		if (RNGSeed >= 0) {
			rsrcRandomizer = new System.Random (RNGSeed);
		} else {
			rsrcRandomizer = new System.Random ();
		}
		GenerateNaturalResources (0);

		if (player == null) {
			player = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	

	void Update () {
		GenerateNaturalResources (1);
	}

	void GenerateNaturalResources(int mode = 0) {
		// Mode 0 - position anywhere
		// Mode 1 - position far away from the player

		// Generate enough trees to satisfy the number of resources desired
		int numRsrcNeeded = numRsrcAroundPlayer - gameObject.transform.childCount;
		for (int i = 0; i < numRsrcNeeded; ++i) {

			float radius;
			float angle;
			// float playerAngle = Vector3.Angle (Vector3.forward, player.transform.forward);


			// Randomly get a radial distance and angle from the player
			// Infinitely do this until radius is more than 30 to avoid instantiating resource where the player is
			// Infinitely do this on mode 1 until radius is more than 75% of the radialMax
			do {
				radius = (float) (rsrcRandomizer.NextDouble() * radialMax);
				angle = (float)(rsrcRandomizer.NextDouble () * 360);
			} while ((mode == 0 && radius < 30) || (mode == 1 && radius < (0.75 * radialMax)));

			// Convert radius and angle to Cartesian coordinates
			float pos_x = radius * Mathf.Cos(angle) + player.transform.position.x;
			float pos_z = radius * Mathf.Sin(angle) + player.transform.position.z;

			// Determine how to rotate the natural resource
			float rotate_x = (float)(rsrcRandomizer.NextDouble () * 40f - 20f);
			float rotate_y = (float)(rsrcRandomizer.NextDouble () * 360f);
			float rotate_z = (float)(rsrcRandomizer.NextDouble () * 40f - 20f);
			Quaternion rand_rot = Quaternion.Euler (rotate_x, rotate_y, rotate_z);

			// Get a random scale factor
			float scaleFactor = (float)(rsrcRandomizer.NextDouble () * 0.7f + 0.8f);

			// Instantiate a new natural resource with random position and rotation
			int rsrc_idx = rsrcRandomizer.Next(0, naturalRsrc.Length);
			GameObject obj = naturalRsrc [rsrc_idx].obj;
			Quaternion rot = naturalRsrc [rsrc_idx].rotation;
			float obj_scale = naturalRsrc [rsrc_idx].scale * scaleFactor;

			GameObject newRsrc = Instantiate(obj, new Vector3(pos_x, 0, pos_z), rot * rand_rot);
			newRsrc.transform.localScale = Vector3.one * obj_scale;

			// Parent the natural resource to the current GameObject
			newRsrc.transform.SetParent (gameObject.transform);

			// Add Collision and NatureManager GameObject
			CapsuleCollider rsrcCollider = newRsrc.AddComponent<CapsuleCollider>();
			rsrcCollider.center = Vector3.zero;
			rsrcCollider.height = 10f;
			rsrcCollider.radius = 0.5f;

			ResourceManager rsrcNatManager = newRsrc.AddComponent<ResourceManager> ();
			rsrcNatManager.player = player;
			rsrcNatManager.dstThreshold = radialMax;
		}
	}
}



[System.Serializable]
public struct EnvResource {
	public GameObject obj;
	public Quaternion rotation;
	public float scale;
}
