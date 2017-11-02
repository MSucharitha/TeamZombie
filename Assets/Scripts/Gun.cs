using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    private int score = 0;
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    Animator anim;
    string GunName;
    GameObject zombie1;
    AnimController2 otherAnimator;
    void Awake()
    {
        zombie1 = GameObject.Find("Zombie_02_black_Tshirt").GetComponent<GameObject>();
        if (zombie1 != null)
        {
           // otherAnimator = zombie1.GetComponent<Animator>();
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        // GunName = this.ToString();      
    }
	
	// Update is called once per frame
	void Update () {
        //   if (Input.GetButtonDown("Fire1"))
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Debug.Log("Pressed left click.");
            System.Console.WriteLine("fired");
        }

    }

    public int getScore() {
        return score;
    }
    private void incrementScore() {
        score += 100;
    }

    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           // 
            Debug.Log(message: hit.transform.name + "Fired");
            print(hit.collider.name);
            System.Console.WriteLine(hit.collider.name);
            System.Console.WriteLine(hit.distance);
            System.Console.WriteLine(GunName);
            System.Console.WriteLine(hit.transform.name);
            otherAnimator = hit.collider.gameObject.GetComponent<AnimController2>();
            if(otherAnimator !=null)
         //   if (hit.collider.gameObject.name == "Zombie_01_Tshirt" || hit.collider.name == "Terrain Chunk" || hit.collider.name == "Zombie_01_Tshirt" || hit.collider.gameObject.name == "Zombie_02_black_Tshirt"|| hit.transform.name == "Zombie_01_Tshirt") //name ?
            {
                
                if (this.gameObject.name.Equals("Handgun"))
                {
                    
                        otherAnimator.anim.SetInteger("life", 0);
                    
                    // otherAnimator.shot1();                   
                    System.Console.WriteLine("handgun shot zombie");

                    incrementScore();


                }
                else
                {                    
                        otherAnimator.anim.SetInteger("life", 0);                    
                   
                    System.Console.WriteLine("weapon shot zombie");

                }

				// Remove Capsule Collider for zombie once it is dead
				hit.collider.gameObject.GetComponent<CapsuleCollider>().enabled = false;
             }
        }
    }
}
