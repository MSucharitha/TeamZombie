using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class Gun : MonoBehaviour {

    public int max_damage = 3;
    public float max_range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    private Animator anim;
    string GunName;
    private Text scoreText;    
    private GameObject levelManager;   
    private LevelManager levelManagerScript;

    void Start () {
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();        
        levelManager = GameObject.Find("LevelManager");
        if (levelManager != null)
        {
            levelManagerScript = levelManager.GetComponent<LevelManager>();
        }
      
        anim = GetComponent<Animator>();

        // GunName = this.ToString();           
       
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetButtonDown("Fire1"))
        {
            Shoot();
          //  Debug.Log("Pressed left click. Bullet fired.");
        }
    }

    
    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;

		bool foundCollision = Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, max_range);

		if (foundCollision) {

			GameObject HitObj = hit.collider.gameObject;
			
			print ("Shot Object Tag: " + HitObj.transform.tag);
			// Debug.Log (message: hit.transform.name + "Fired");
			// print (hit.collider.name);
			// System.Console.WriteLine (hit.collider.name);
		
			if (HitObj.transform.tag == "zombie") {

				// Call the shooting function for the zombie
				AnimController2 zombieCtrl = HitObj.GetComponent<AnimController2> ();

                
                int shot_damage = 1;
                if (hit.distance < max_range)
                    shot_damage = (int) ((1 - hit.distance / max_range) * max_damage);                

                if (hit.point.y >= hit.collider.bounds.size.y * 5 / 8)
                {
                    shot_damage = Mathf.Min(max_damage, shot_damage * 2);
                    Debug.Log("zombie head shot!");

                    // add bonus
                    //scoreText.text = "Bonus Score: " + score + "";
                }
                Debug.Log("damage: " + shot_damage);
                zombieCtrl.shoot (shot_damage);

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
