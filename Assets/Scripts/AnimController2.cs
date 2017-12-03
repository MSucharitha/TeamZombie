using System.Collections;
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
    private GameObject zombieManager;
    private Manager zombieManagerScript;
    private CharacterController zombieController;

	public GameObject healthbarObject;

	private float lastShootTime = -100f;

    void Start()
    {
        anim = GetComponent<Animator>();

		if (healthbarObject != null) {
			Debug.Log("Life is " + anim.GetInteger ("life").ToString());
			healthbarObject.GetComponent<HealthUI> ().SetHealth (anim.GetInteger ("life"));
		}

        // Set a character controller if necessary
        zombieController = GetComponent<CharacterController>();
        if (zombieController == null)
        {
            zombieController = gameObject.AddComponent<CharacterController>();
            
            // Set Character Controller properties for the zombie
            zombieController.center = new Vector3(0f, 0.75f, 0f);
            zombieController.radius = 0.5f;
            zombieController.height = 1.6f;
        }

        // Find player point-of-reference for movement script 
        player = GameObject.FindWithTag("Player");
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        //find zombie and level manager
        levelManager = GameObject.Find("LevelManager");
        if (levelManager != null)
        {
            levelManagerScript = levelManager.GetComponent<LevelManager>();
        }
        zombieManager = GameObject.Find("EnemyManager");
        if (zombieManager != null)
        {
            zombieManagerScript = zombieManager.GetComponent<Manager>();
        }
    }

    // Update is called once per frame
    void Update() {
		bool zombieAlive = anim.GetInteger ("life") > 0;

        if(zombieAlive) {
            // If the zombie is still alive...

            // Walk forwards
            Vector3 moveDirection = Vector3.forward;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            zombieController.Move(moveDirection * Time.deltaTime);

            // Rotate towards player
            if (player != null) {

                // Set all zombies to always follow player
                float playerAngle = Vector3.SignedAngle(transform.forward, player.transform.position - transform.position, Vector3.up);
                float rotateSpeed = playerAngle * Time.deltaTime;
                transform.Rotate(0f, rotateSpeed, 0f, Space.Self);

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

            }
        }

		if (healthbarObject != null) {
			float currTime = Time.time;
			float dstToPlayer = Vector3.Distance (transform.position, player.transform.position);
			if (dstToPlayer <= 25 && anim.GetInteger("life") > 0) {
				healthbarObject.GetComponent<HealthUI> ().Show ();
			} else if (currTime - lastShootTime > 5f || anim.GetInteger ("life") <= 0) {
				healthbarObject.GetComponent<HealthUI> ().Hide ();
			}
		}
    }

	public void shoot(int damage) {
		// TODO: REMOVE NEXT LINE
		damage = 1;

		// Calculate the HP of the zombie after damage is taken
		int HP = anim.GetInteger ("life");
		Debug.Log ("Health was " + HP.ToString ());
		HP = Mathf.Max(HP - damage, 0);
		Debug.Log ("Health after damage is " + HP.ToString ());
		Debug.Log ("Zombie HP: " + anim.GetInteger("life") + ", " + HP);

		// Update the HP/Life points of the zombie
		anim.SetInteger ("life", HP);

		// Update the score
		levelManagerScript.incrementScore(damage * 10);

		// What to do when the zombie is dead (has no HP left)
		if (HP == 0) {

			// Disable the Character Controller's collider
			if (zombieController != null) {
				zombieController.enabled = false;
			}

			// Queue Zombie Object to disappear from screen after a set amount of time
			Destroy(this.gameObject, zombieDeathTimeout);

            //decrement spawn count             
            if (zombieManagerScript != null)
            {
                zombieManagerScript.decrementSpawnCount();
                Debug.Log("zombie killed, " + "current zombie count: " + zombieManagerScript.getSpawnCount());
            }

            //increment score here
            levelManagerScript.incrementScore(100);
			levelManagerScript.OnZombieKill ();

			// Stop showing arrow on zombie death

            //TODO different zombie type for different points
        }


		// Update health bar and show
		if (healthbarObject != null) {
			healthbarObject.GetComponent<HealthUI> ().UpdateHealth (HP);
			float currTime = Time.time;
			if (currTime - lastShootTime > 5f) {
				lastShootTime = currTime;
				healthbarObject.GetComponent<HealthUI> ().Show ();
			}
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
