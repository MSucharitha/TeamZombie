using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSHealth : MonoBehaviour {
    [SerializeField] public int index;
    public ApplicationModel app;
    private Text healthText;

    public int gameOverSceneIndex = 2;
    public void healthDamaged()
    {
        int health = 0;
        System.Int32.TryParse(healthText.text.Split(':')[1].TrimEnd('%'), out health);
        if (health - 1 <= 0)
        {
            die();
        }
        else
        {
            health -= 1;
            healthText.text = "Health : " + health + "%";
        }


    }
    public void die()
    {
        Text scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
        int score = 0;
        System.Int32.TryParse(scoreText.text.Split(':')[1], out score);
        ApplicationModel.score = score;
        SceneManager.LoadScene(gameOverSceneIndex);
    }
    void OnTriggerEnter(Collider col)
    {
//        Debug.Log("collision detected");

        if (col.gameObject.GetComponent<AnimController2>() != null)
        {
            if (healthText == null)
            {
                healthText=GameObject.FindGameObjectWithTag("health").GetComponent<Text>();
            }
            healthDamaged();
            Debug.Log("Player-Zombie collision");
            
        }
    }
    void OnTriggerStay(Collider col)
    {
        //        Debug.Log("collision detected");

        if (col.gameObject.GetComponent<AnimController2>() != null)
        {
            if (healthText == null)
            {
                healthText = GameObject.FindGameObjectWithTag("health").GetComponent<Text>();
            }
            healthDamaged();
            Debug.Log("Player-Zombie collision");

        }
    }

}
