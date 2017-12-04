using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSHealth : MonoBehaviour {
    [SerializeField] public int index;
    public ApplicationModel app;
    void OnTriggerEnter(Collider col)
    {
//        Debug.Log("collision detected");

        if (col.gameObject.GetComponent<AnimController2>() != null)
        {
            Debug.Log("Player-Zombie collision");
            Text scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
            int score = 0;
            System.Int32.TryParse(scoreText.text.Split(':')[1], out score);
            ApplicationModel.score = score;
            SceneManager.LoadScene(2);
        }
    }
}
