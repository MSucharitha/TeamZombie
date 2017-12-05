using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverScore : MonoBehaviour {

	// Use this for initialization
	Text score;
	void Start () {
		score = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update()
	{
		score.text = "Your Score : "+ApplicationModel.score + "";

	}
}
