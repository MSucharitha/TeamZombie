﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    private int score = 0; // should be in player controller
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    Animator anim;
    string GunName;
    private Text scoreText;

    // Use this for initialization
    void Start () {
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();

        anim = GetComponent<Animator>();
        // GunName = this.ToString();      
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Debug.Log("Pressed left click. Gun fired.");
        }
    }

    public int getScore() {
        return score;
    }

    private void incrementScore() {
        score += 100;
        scoreText.text = "Score: " + score + "";
    }

    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;

		bool foundCollision = Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range);

		if (foundCollision) {

			GameObject HitObj = hit.collider.gameObject;
			
			print ("Shot Object Tag: " + HitObj.transform.tag);
			// Debug.Log (message: hit.transform.name + "Fired");
			// print (hit.collider.name);
			// System.Console.WriteLine (hit.collider.name);
			// System.Console.WriteLine (hit.distance);
			// System.Console.WriteLine (GunName);
			// System.Console.WriteLine (hit.transform.name);

			if (HitObj.transform.tag == "zombie") {

				// Call the shooting function for the zombie
				AnimController2 zombieCtrl = HitObj.GetComponent<AnimController2> ();
				zombieCtrl.shoot (1);
				incrementScore();

                //// Legacy code for later consideration of weapon type
                /*
				if (this.gameObject.name.Equals ("Handgun")) {

					otherAnimator.anim.SetInteger ("life", 0);

					// otherAnimator.shot1();                   
					System.Console.WriteLine ("handgun shot zombie");

					


				} else {                    
					otherAnimator.anim.SetInteger ("life", 0);                    

					System.Console.WriteLine ("weapon shot zombie");

				}
				*/
			}
		}
    }
}
