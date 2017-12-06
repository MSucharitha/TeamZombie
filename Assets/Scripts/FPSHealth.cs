using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FPSHealth : MonoBehaviour {
    [SerializeField] public int index;
    public ApplicationModel app;

	public int gameOverSceneIndex = 2;
    public int damageLevel;
    public GameObject updatePlayerHealth;
    public GameObject getPlayerHealth;
    private float checkhealth;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("collision detected");
        if (updatePlayerHealth != null)
        {
            Debug.Log("call the OnPlayerAttacked method in LevelManager.cs");
            updatePlayerHealth.GetComponent<LevelManager>().OnPlayerAttacked();
        }
        //checkhealth = getPlayerHealth.GetComponent<FPShealthUI>().healthPercentage;
        Debug.Log("player checkhalth" + checkhealth);
        if ((col.gameObject.GetComponent<AnimController2>() != null ) && (getPlayerHealth.GetComponent<FPShealthUI>().healthPercentage <= 0.0f))
        {
            Debug.Log("Player-Zombie collision");
            Text scoreText = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
            int score = 0;
            System.Int32.TryParse(scoreText.text.Split(':')[1], out score);
            ApplicationModel.score = score;
			SceneManager.LoadScene(gameOverSceneIndex);
        }
    }
}
