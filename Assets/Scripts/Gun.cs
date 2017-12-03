using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public int max_damage = 5;
    public float max_range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    private Animator anim;
    string GunName;
    private Text scoreText;
    private GameObject zombieManager;
    private GameObject levelManager;
    private Manager zombieManagerScript;
    private LevelManager levelManagerScript;
    
    void Start () {
        scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        levelManager = GameObject.FindWithTag("system");
        if (levelManager != null)
        {
            levelManagerScript = levelManager.GetComponent<LevelManager>();
        }
        zombieManager = GameObject.Find("EnemyManager");
        if (zombieManager != null)
        {
            zombieManagerScript = zombieManager.GetComponent<Manager>();
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

                //decrement spawn count 
                //is it working?
                if (zombieManagerScript != null)
                {
                    zombieManagerScript.spawnCount--;
                    Debug.Log("zombie killed, " + "current zombie count: " + zombieManagerScript.spawnCount);
                }
                int shot_damage = 1;
                if (hit.distance < max_range)
                    shot_damage = (int) ((1 - hit.distance / max_range) * max_damage);                
                Debug.Log("hit point y: " + hit.point.y);
                Debug.Log("collider height: " + hit.collider.bounds.size.y);
                if (hit.point.y >= hit.collider.bounds.size.y * 5 / 8)
                {
                    shot_damage = Mathf.Min(max_damage, shot_damage * 2);
                    Debug.Log("zombie head shot!");
                    // add bonus
                    //scoreText.text = "Bonus Score: " + score + "";
                }
                Debug.Log("damage: " + shot_damage);
                zombieCtrl.shoot (shot_damage);
                //increment score, or increment when zombie is dead???
                levelManagerScript.incrementScore(shot_damage*10);

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
