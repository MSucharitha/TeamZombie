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
    public GameObject zombieArrowPrefab;                // The enemy prefab to be spawned.
    private GameObject zombieArrow;
    public Transform playerArrow;


    //maximum allowed number of objects - set in the editor
    public int maxObjects = 30;

    //number of objects currently spawned
    public int spawnCount = 0;

   // public void decrementSpawnCount() { }

    void Start()  
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
       
        //zombieArrow = Instantiate(zombieArrowPrefab) as GameObject;
       // GameObject obj = Instantiate(prefab, position, rotation) as GameObject;

    }

    void Spawn()
    {

        if (maxObjects > spawnCount)
        {
            SpawnEnemy();
        }

    }
        
    void SpawnEnemy(){

    // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        int offsetX = Random.Range(50, 100);
        int offsetZ = Random.Range(50, 100);
        int signX = Random.Range(0, 1);
        if (signX == 0) { signX = -1; };
        int signZ = Random.Range(0, 1);
        if (signZ == 0) { signZ = -1; };
        int random_zombie = Random.Range(0, zombies.Length);

        int random_angle = Random.Range(1, 360);

        // Jessie original version
        
        spawnPoints[spawnPointIndex].position = new Vector3(playerLocation.position.x + offsetX * signX, 0, playerLocation.position.z + offsetZ * signZ);

       // Vector3 targetDir = target.position - transform.position;
        Vector3 targetDir = playerLocation.position - spawnPoints[spawnPointIndex].position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        spawnPoints[spawnPointIndex].rotation.Set(spawnPoints[spawnPointIndex].rotation.x, angle, spawnPoints[spawnPointIndex].rotation.z, spawnPoints[spawnPointIndex].rotation.w);
        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //gameobject new_zombie = Instantiate(...);
        // new_zombie.(become child of some object)(parent object name)        
        GameObject newZombie = Instantiate(zombies[random_zombie], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newZombie.tag = "zombie";
        spawnCount++;

//viraj version

              
        //Vector3 directionOfLook = playerLocation.position - position;
        //Quaternion rotate = Quaternion.LookRotation(directionOfLook);
      
        //Quaternion rotation = new Quaternion(playerArrow.rotation.x, playerLocation.rotation.y + angle-90, spawnPoints[spawnPointIndex].rotation.z, spawnPoints[spawnPointIndex].rotation.w+90);
  //need this later      Quaternion rotation = new Quaternion(playerArrow.rotation.x, playerArrow.rotation.y, playerArrow.rotation.z, playerArrow.rotation.w);
        Vector3 position = new Vector3(playerLocation.position.x + offsetX * signX, 17.3f, playerLocation.position.z + offsetZ * signZ);
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //need this later     zombieArrow = Instantiate(zombieArrowPrefab, position, rotation) as GameObject;
        //need this later    zombieArrow.transform.localScale = playerArrow.localScale;
       
    }

}

