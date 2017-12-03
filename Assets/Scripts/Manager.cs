using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is zombie manager
public class Manager : MonoBehaviour
{
    //  public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public Transform playerLocation;
    public GameObject[] zombies;                // The enemy prefab to be spawned.
    public float spawnTime;            // 2 seconds between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject zombieArrowPrefab;                // The enemy prefab to be spawned.
    private GameObject zombieArrow;
    public Transform playerArrow;
	public GameObject zombieSpawnsParent;

    public GameObject healthBar;

	public int maxObjects = 30;  

    public void setMaxObjects(int num) {
        this.maxObjects = num;
    }

    // number of objects currently spawned
    private int spawnCount = 5;

    public void resetSpawnCount() {
        spawnCount = 0;
    }

    public int getSpawnCount()
    {
        return this.spawnCount;
    }

    public void incrementSpawnCount() {
        this.spawnCount++;
    }
    public void decrementSpawnCount() {
        this.spawnCount--;
    }

    void Start()  
    {
        InvokeRepeating("Spawn", 0, spawnTime);  

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

        int offsetX = Random.Range(60, 100);
        int offsetZ = Random.Range(60, 100);
        int signX = Random.Range(0, 1);
        if (signX == 0) { signX = -1; };
        int signZ = Random.Range(0, 1);
        if (signZ == 0) { signZ = -1; };
        int random_zombie = Random.Range(0, zombies.Length);

        int random_angle = Random.Range(1, 360);       
        
        spawnPoints[spawnPointIndex].position = new Vector3(playerLocation.position.x + offsetX * signX, 0, playerLocation.position.z + offsetZ * signZ);

       // Vector3 targetDir = target.position - transform.position;
        Vector3 targetDir = playerLocation.position - spawnPoints[spawnPointIndex].position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        spawnPoints[spawnPointIndex].rotation.Set(spawnPoints[spawnPointIndex].rotation.x, angle, spawnPoints[spawnPointIndex].rotation.z, spawnPoints[spawnPointIndex].rotation.w);
        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject newZombie = Instantiate(zombies[random_zombie], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newZombie.tag = "zombie";
        
		newZombie.transform.SetParent (zombieSpawnsParent.transform);
        incrementSpawnCount();

        // Add resource manager to remove zombies that are too far away from the player
//		ResourceManager rsrcManager = newZombie.AddComponent<ResourceManager> ();
//		rsrcManager.dstThreshold = 100f;
        
        GameObject zombieHealthBar = Instantiate(healthBar, (Vector3.up * 1.6f * newZombie.transform.localScale.x), Quaternion.Euler(Vector3.zero));
        zombieHealthBar.transform.parent = newZombie.transform;
        zombieHealthBar.transform.Translate(newZombie.transform.position);

        Debug.Log("zombie created, " + "current zombie count: " + this.spawnCount);
		zombieArrow = Instantiate (zombieArrowPrefab, newZombie.transform.position, newZombie.transform.rotation, newZombie.transform ) as GameObject;
		zombieArrow.transform.localScale = playerArrow.localScale;



    }

}

