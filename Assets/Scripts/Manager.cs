using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    //  public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public Transform playerLocation;
    public GameObject zombie;
    public GameObject[] zombies;                // The enemy prefab to be spawned.
    public float spawnTime = 5f;            // 2 seconds between each spawn.
    public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
    public GameObject zombieArrowPrefab;                // The enemy prefab to be spawned.
    private GameObject zombieArrow;
    public Transform playerArrow;
	public GameObject zombieSpawnsParent;

    // maximum allowed number of objects - set in the editor
    public int maxObjects = 20;

    // number of objects currently spawned
    public int spawnCount = 0;

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
        GameObject newZombie = Instantiate(zombies[random_zombie], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        newZombie.tag = "zombie";
		newZombie.transform.SetParent (zombieSpawnsParent.transform);
        spawnCount++;

		CapsuleCollider zombieCollider = newZombie.GetComponent<CapsuleCollider> ();
		if (zombieCollider == null) {
			// Add a capsule collider to the attached Zombie GameObject
			zombieCollider = newZombie.AddComponent<CapsuleCollider> ();

			// Set the collider for the zombie's dimensions
			zombieCollider.height = 2f;
			zombieCollider.radius = 0.5f;
			zombieCollider.center = new Vector3 (0f, 0.75f, 0f);

			// Enable the collider
			zombieCollider.enabled = true;
		}

//viraj version
/*
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
*/
    }

}

