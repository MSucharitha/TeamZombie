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
    
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        // Add a capsule collider to the attached Zombie GameObject
        CapsuleCollider zombieCollider;
        if (gameObject.GetComponent<CapsuleCollider>() == null)
        {
            zombieCollider = gameObject.AddComponent<CapsuleCollider>();

            // // If Unity properly determines the height and radius, then remove this section
            zombieCollider.height = 2f;
            zombieCollider.radius = 0.5f;
            zombieCollider.center = new Vector3(0f, 0.75f, 0f);
        }
        else
        {
            zombieCollider = gameObject.GetComponent<CapsuleCollider>();
            zombieCollider.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetInteger("life")>0)
    //    if (keepwalking)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            //   transform.Translate(Vector3.back * Time.deltaTime * 10);
        }
        /*
        if (transform.position.z <= -241) {
            anim.SetInteger("life", 1);
        }
        if (transform.position.z <= -245)
        {
            shot0();
            keepwalking = false;
        }*/
    }

    public void shot1()
    {
        anim.SetInteger("life", 1);
    }
    public void shot0()
    {        
        anim.SetInteger("life", 0);
    }

}
