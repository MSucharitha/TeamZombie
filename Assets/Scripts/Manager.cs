using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //  public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public Transform playerLocation;
    public GameObject zombie;                // The enemy prefab to be spawned.
    public float spawnTime = 5f;            // 2 seconds between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject zombieArrowPrefab;                // The enemy prefab to be spawned.
    private GameObject zombieArrow;
    public Transform playerArrow;


    void Start()  
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
       
        //zombieArrow = Instantiate(zombieArrowPrefab) as GameObject;
       // GameObject obj = Instantiate(prefab, position, rotation) as GameObject;

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

        int angle = Random.Range(1, 360);

        spawnPoints[spawnPointIndex].position = new Vector3(playerLocation.position.x + offsetX * signX, 0, playerLocation.position.z + offsetZ * signZ);
        //  spawnPoints[spawnPointIndex].rotation.y = playerLocation.rotation.y + 180;
        Vector3 position = new Vector3(playerLocation.position.x + offsetX * signX, 17.3f, playerLocation.position.z + offsetZ * signZ);
        //Vector3 directionOfLook = playerLocation.position - position;
        //Quaternion rotate = Quaternion.LookRotation(directionOfLook);
        spawnPoints[spawnPointIndex].rotation.Set(spawnPoints[spawnPointIndex].rotation.x, playerLocation.rotation.y + angle, spawnPoints[spawnPointIndex].rotation.z, spawnPoints[spawnPointIndex].rotation.w);

        //Quaternion rotation = new Quaternion(playerArrow.rotation.x, playerLocation.rotation.y + angle-90, spawnPoints[spawnPointIndex].rotation.z, spawnPoints[spawnPointIndex].rotation.w+90);
        Quaternion rotation = new Quaternion(playerArrow.rotation.x, playerArrow.rotation.y, playerArrow.rotation.z, playerArrow.rotation.w);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        zombieArrow = Instantiate(zombieArrowPrefab, position, rotation) as GameObject;
        zombieArrow.transform.localScale = playerArrow.localScale;
        zombie=Instantiate(zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
    }

}

