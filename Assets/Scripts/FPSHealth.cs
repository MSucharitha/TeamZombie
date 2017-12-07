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
    public void healthDamaged(int damage)
    {
        int health = 0;
        if (healthText == null)
        {
            healthText = GameObject.FindGameObjectWithTag("health").GetComponent<Text>();
        }
        System.Int32.TryParse(healthText.text.Split(':')[1].TrimEnd('%'), out health);
        if (health - 1 <= 0)
        {
            die();
        }
        else
        {
            health -= damage;
            healthText.text = "Health : " + health + "%";
        }

       // Debug.Log("current health " + health);
    }


    public void healthIncreaseBonus(int bonus)
    {
        int health = 0;
        if (healthText == null)
        {
            healthText = GameObject.FindGameObjectWithTag("health").GetComponent<Text>();
        }
        System.Int32.TryParse(healthText.text.Split(':')[1].TrimEnd('%'), out health);
        if(health<100)
        {
            health = Mathf.Min(100, health + bonus);
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
            healthDamaged(5);
            //Debug.Log("Player-Zombie collision");
           
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
  //          healthDamaged(1);
            //Debug.Log("Player-Zombie collision");

        }
    }

}
