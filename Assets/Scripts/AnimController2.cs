﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController2 : MonoBehaviour {

    Animator anim;
    public float speed = 0.8f;
    public float rotationSpeed = 75.0f;
    bool keepwalking = true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (keepwalking)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            //   transform.Translate(Vector3.back * Time.deltaTime * 10);
        }
        if (transform.position.z <= -241) {
            anim.SetInteger("life", 1);
        }
        if (transform.position.z <= -245)
        {
            anim.SetInteger("life", 0);
            keepwalking = false;
        }
    }
}
