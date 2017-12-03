using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSHealth : MonoBehaviour {
    [SerializeField] public int index;

    void OnTriggerEnter(Collider col)
    {
//        Debug.Log("collision detected");

        if (col.gameObject.GetComponent<AnimController2>() != null)
        {
            Debug.Log("Player-Zombie collision");
//            SceneManager.LoadScene(index);
        }
    }
}
