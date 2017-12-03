﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimController2 : MonoBehaviour {

    public Animator anim;
    public float speed = 0.8f;
    public float rotationSpeed = 75.0f;
    bool keepwalking = true;
    public Transform playerLocation;
	static float playerDistanceThreshold = 50f;
	static float zombieDeathTimeout = 5f;
    private GameObject player;
    private Text scoreText;
    private GameObject levelManager;   
    private LevelManager levelManagerScript;
    void Start()
    {
        anim = GetComponent<Animator>();
		CapsuleCollider zombieCollider = GetComponent<CapsuleCollider> ();
		if (zombieCollider == null) {
			// Add a capsule collider to the attached Zombie GameObject
			zombieCollider = gameObject.AddComponent<CapsuleCollider> ();

			// Set the collider for the zombie's dimensions
			zombieCollider.height = 2f;
			zombieCollider.radius = 0.5f;
			zombieCollider.center = new Vector3 (0f, 0.75f, 0f);

			// Enable the collider
			zombieCollider.enabled = true;
		}
        player = GameObject.FindWithTag("Player");
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        levelManager = GameObject.Find("LevelManager");
        if (levelManager != null)
        {
            levelManagerScript = levelManager.GetComponent<LevelManager>();
        }
    }

    // Update is called once per frame
    void Update() {
		bool zombieAlive = anim.GetInteger ("life") > 0;

        if(zombieAlive) {
			// If the zombie is still alive...

			// Walk forwards along the terrain
			Vector3 desiredMove = Vector3.forward * speed * Time.deltaTime;
//			RaycastHit hitInfo;
//			Physics.SphereCast (transform.position, 0.5f, Vector3.down, out hitInfo, 3f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
//			desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized * speed * Time.deltaTime;
//			Debug.Log (desiredMove.ToString());

			// TODO: Make the zombie walk along the floor rather than through it
			transform.Translate (desiredMove, Space.Self);

			// Rotate towards player only if the player is near and within viewing range
			
			if (player != null) {

//				// Check if player is near the zombie
//				float playerDistance = Vector3.Distance (player.transform.position, transform.position);
//				if (playerDistance < playerDistanceThreshold) {
//
//					// Determine how to rotate towards the player
//					float playerAngle = Vector3.SignedAngle (transform.forward, player.transform.position - transform.position, Vector3.up);
//					float playerAngleSign = playerAngle / Mathf.Abs (playerAngle);
//
//					if (playerAngle < 70f && playerAngle > -70f) {
//						// (-70f, 70f) is an arbitary range, but assumes that the player is within the zombie's viewport
//
//						// Rotate towards the player gradually
//						float rotateSpeed = Mathf.Min(Mathf.Abs(playerAngle), 5f) * playerAngleSign * Time.deltaTime;
//						transform.Rotate (0f, rotateSpeed, 0f, Space.Self);
//
//						// TODO: Set animation to aggressive mode
//					} // otherwise, the zombie doesn't see the player so the zombie ignores the player
//				}

				// Set all zombies to always follow player
				float playerAngle = Vector3.SignedAngle (transform.forward, player.transform.position - transform.position, Vector3.up);
				float rotateSpeed = playerAngle * Time.deltaTime;
				transform.Rotate (0f, rotateSpeed, 0f, Space.Self);
			}
        }
    }

	public void shoot(int damage) {
		// Calculate the HP of the zombie after damage is taken
		int HP = anim.GetInteger ("life");
		HP = Mathf.Max(HP - damage, 0);
		Debug.Log ("Zombie HP: " + anim.GetInteger("life") + ", " + HP);

		// Update the HP/Life points of the zombie
		anim.SetInteger ("life", HP);

		// What to do when the zombie is dead (has no HP left)
		if (HP == 0) {

			// Disable the capsule collider
			CapsuleCollider zombieCollider = GetComponent<CapsuleCollider> ();
			if (zombieCollider != null) {
				zombieCollider.enabled = false;
			}

			// Queue Zombie Object to disappear from screen after a set amount of time
			Destroy(this.gameObject, zombieDeathTimeout);

            //increment score here
            levelManagerScript.incrementScore(100);
            //TODO different zombie type for different points
        }
	}

    public void shot1() {
        anim.SetInteger("life", 1);
    }
    public void shot0()
    {
        anim.SetInteger("life", 0);
    }

}
