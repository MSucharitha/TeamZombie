using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //  public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public Transform playerLocation;
    public GameObject[] zombies;                // The enemy prefab to be spawned.
    public float spawnTime = 5f;            // 2 seconds between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    

    void Start()  
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {        

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        int offsetX = Random.Range(30, 60);
        int offsetZ = Random.Range(30, 60);
        int signX = Random.Range(0, 1);
        if (signX == 0) { signX = -1; };
        int signZ = Random.Range(0, 1);
        if (signZ == 0) { signZ = -1; };
        int random_zombie = Random.Range(0, zombies.Length);

        int angle = Random.Range(1, 360);

        spawnPoints[spawnPointIndex].position = new Vector3(playerLocation.position.x + offsetX * signX, 0, playerLocation.position.z + offsetZ * signZ);
        spawnPoints[spawnPointIndex].rotation.Set(spawnPoints[spawnPointIndex].rotation.x, /*playerLocation.rotation.y*/ + angle, spawnPoints[spawnPointIndex].rotation.z, spawnPoints[spawnPointIndex].rotation.w);
      //  spawnPoints[spawnPointIndex].rotation.y = playerLocation.rotation.y + 180;
        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //gameobject new_zombie = Instantiate(...);
        // new_zombie.(become child of some object)(parent object name)
        Instantiate(zombies[random_zombie], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

}

