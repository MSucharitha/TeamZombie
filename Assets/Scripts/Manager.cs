using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is zombie manager
public class Manager : MonoBehaviour
{
    //  public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public Transform playerLocation;
    public GameObject[] zombies;                // The enemy prefab to be spawned.    
    public Transform spawnPoint;
    private float spawnTime = 5;            // 2 seconds between each spawn.
    private int maxObjects = 20;

    public GameObject zombieArrowPrefab;                // The enemy prefab to be spawned.
    private GameObject zombieArrow;
    public Transform playerArrow;
	public GameObject zombieSpawnsParent;
    public GameObject healthBar;
	

    public void setMaxObjects(int num) {
        this.maxObjects = num;
    }

    // number of objects currently spawned, starting at 0 or 5?
    private int spawnCount = 0;

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


    public void setSpawnTime(float newTime)
    {
        this.spawnTime = newTime;
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

    private int zombieTypeLowerBound = 0;
    private int zombieTypeUpperBound = 1;
    public void addZombieTypes(int level) {
        //todo length - 1?
        if (level <= 3)
        {
            zombieTypeUpperBound = Mathf.Min(zombies.Length, level);
        }
        else if (level == 4)
        {
            zombieTypeUpperBound = Mathf.Min(zombies.Length, 5);
        }
        else if (level == 5)
        {
            zombieTypeUpperBound = Mathf.Min(zombies.Length, 7);
        }
        else if (level == 6)
        {
            zombieTypeLowerBound = 2;
            zombieTypeUpperBound = Mathf.Min(zombies.Length, 8);
        }
        else if (level == 7)
        {
            zombieTypeLowerBound = 4;
            zombieTypeUpperBound = Mathf.Min(zombies.Length, 8);
        }
        else {
            zombieTypeLowerBound = 6;
            zombieTypeUpperBound = Mathf.Min(zombies.Length, 8);
        }

    }


    void SpawnEnemy(){

        int randomRadius = Random.Range(40, 75);
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
       
        int random_zombie = Random.Range(zombieTypeLowerBound, zombieTypeUpperBound);
        Debug.Log("random type " + random_zombie);
        int random_angle = Random.Range(0, 360);
        spawnPoint.position = new Vector3(playerLocation.position.x + randomRadius * Mathf.Cos(randomAngle), 0, playerLocation.position.z + randomRadius * Mathf.Sin(randomAngle));

       // Vector3 targetDir = target.position - transform.position;
        Vector3 targetDir = playerLocation.position - spawnPoint.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        spawnPoint.rotation.Set(spawnPoint.rotation.x, angle, spawnPoint.rotation.z, spawnPoint.rotation.w);
        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject newZombie = Instantiate(zombies[random_zombie], spawnPoint.position, spawnPoint.rotation);
        newZombie.tag = "zombie";
        
		newZombie.transform.SetParent (zombieSpawnsParent.transform);
		Vector3 scalePrefab = newZombie.transform.localScale;
		Vector3 scaleParent = transform.localScale;
		newZombie.transform.localScale = new Vector3 (scalePrefab.x * scaleParent.x, scalePrefab.y * scaleParent.y, scalePrefab.z * scaleParent.z);
        incrementSpawnCount();

        // Add resource manager to remove zombies that are too far away from the player
//		ResourceManager rsrcManager = newZombie.AddComponent<ResourceManager> ();
//		rsrcManager.dstThreshold = 100f;
        
		// Add a health bar to each zombie
        GameObject zombieHealthBar = Instantiate(healthBar, (Vector3.up * 1.6f * newZombie.transform.localScale.x), Quaternion.Euler(Vector3.zero));
        zombieHealthBar.transform.parent = newZombie.transform;
        zombieHealthBar.transform.Translate(newZombie.transform.position);

		zombieHealthBar = zombieHealthBar.transform.GetChild (0).gameObject;

		AnimController2 zombieAnimController = newZombie.GetComponent<AnimController2> ();
		zombieAnimController.healthbarObject = zombieHealthBar;
		zombieHealthBar.GetComponent<HealthUI> ().Hide ();

        //Debug.Log("zombie created, " + "current zombie count: " + this.spawnCount);
		zombieArrow = Instantiate (zombieArrowPrefab, newZombie.transform.position, newZombie.transform.rotation, newZombie.transform ) as GameObject;
		zombieArrow.transform.localScale = playerArrow.localScale;
		Vector3 arrowPos = zombieArrow.transform.position;

		zombieAnimController.arrowObject = zombieArrow;



    }

}

