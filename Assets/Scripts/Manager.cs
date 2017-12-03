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
    public Transform spawnPoint;         // An array of the spawn points this enemy can spawn from.
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

    public void setSpawnTime(float newTime) {
        this.spawnTime = newTime;
    }


    void SpawnEnemy(){

    // Find a random index between zero and one less than the number of spawn points.
      //  int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        int randomRadius = Random.Range(60, 100);
        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        int signX = Random.Range(0, 1);
        if (signX == 0) { signX = -1; };
        int signZ = Random.Range(0, 1);
        if (signZ == 0) { signZ = -1; };
        int random_zombie = Random.Range(0, zombies.Length);
        int random_angle = Random.Range(0, 360);
        Debug.Log("random radius: " + randomRadius + " random x: " + randomRadius * Mathf.Cos(randomAngle) + " random z: " +  randomRadius * Mathf.Sin(randomAngle));
        spawnPoint.position = new Vector3(playerLocation.position.x + randomRadius * Mathf.Cos(randomAngle), 0, playerLocation.position.z + randomRadius * Mathf.Sin(randomAngle));

       // Vector3 targetDir = target.position - transform.position;
        Vector3 targetDir = playerLocation.position - spawnPoint.position;
        float angle = Vector3.Angle(targetDir, transform.forward);
        spawnPoint.rotation.Set(spawnPoint.rotation.x, angle, spawnPoint.rotation.z, spawnPoint.rotation.w);
        
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        GameObject newZombie = Instantiate(zombies[random_zombie], spawnPoint.position, spawnPoint.rotation);
        newZombie.tag = "zombie";
        
		newZombie.transform.SetParent (zombieSpawnsParent.transform);
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

        Debug.Log("zombie created, " + "current zombie count: " + this.spawnCount);
		zombieArrow = Instantiate (zombieArrowPrefab, newZombie.transform.position, newZombie.transform.rotation, newZombie.transform ) as GameObject;
		zombieArrow.transform.localScale = playerArrow.localScale;
		Vector3 arrowPos = zombieArrow.transform.position;

		zombieAnimController.arrowObject = zombieArrow;



    }

}

