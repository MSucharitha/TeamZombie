using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCollider : MonoBehaviour {
    GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }
    void OnTriggerEnter(Collider col)
     {

        //System.Console.WriteLine(value:"Collision detected!");
        Debug.Log("collision detected");
        if (col.gameObject.GetComponent<AnimController2>() != null)
        {
            // ... the player is in range.
            Debug.Log("Player dead");
            //System.Console.WriteLine(value: "Player dead");

        }
    }
}
