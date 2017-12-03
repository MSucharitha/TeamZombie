using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int numZombiesKilled;
	public int score;
	public int playerHealth;

	public int level;
	public LevelProps[] levelProps;

	// Managers and GameObjects necessary to run this

	// Use this for initialization
	void Start () {
		level = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartNewLevel() {
		level += 1;

		// Reset Zombie Spawn Point and Increase Num Zombie Spawns

	}

	void OnZombieKill() {
		// Increment the score

	}

	void OnPlayerAttacked() {
		// Decrement the player health

	}

	[System.Serializable]
	public struct LevelProps {
		public int maxZombies;
		public int zombieKillRequirement;
		public int gunsAvailable;
	}
}
