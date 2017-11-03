using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHealth : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("collision detected");

        if (col.gameObject.GetComponent<AnimController2>() != null)
        {
            Debug.Log("Player-Zombie collision");
        }
    }
}
