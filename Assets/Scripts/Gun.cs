using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleflash;
    Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //   if (Input.GetButtonDown("Fire1"))
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {

            Shoot();
            Debug.Log("Pressed left click.");
           
        }

    }
    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           // anim.SetInteger("life", 0);
            Debug.Log(message: hit.transform.name + "Fired");
        }
    }
}
