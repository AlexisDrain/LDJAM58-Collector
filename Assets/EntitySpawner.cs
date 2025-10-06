using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject Spawn;

    public float defaultTimeToSpawn = 6f;
    private float currentTimeToSpawn;
    void Start()
    {
        currentTimeToSpawn = defaultTimeToSpawn;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (currentTimeToSpawn >= 0f) {
            currentTimeToSpawn -= Time.deltaTime;
        }
        if(currentTimeToSpawn <= 0f) {

            // spawn
            currentTimeToSpawn = defaultTimeToSpawn;

            GetComponent<AudioSource>().PlayWebGL();

            GameObject newSpawn = GameObject.Instantiate(Spawn, transform);
            newSpawn.transform.position = transform.position;
            if (newSpawn.GetComponent<Rigidbody>()) {
                newSpawn.GetComponent<Rigidbody>().position = transform.position;
            }
            if (newSpawn.GetComponent<EntityHealth>()) {
                newSpawn.GetComponent<EntityHealth>().spawnedByWave = true;
            }
            if (newSpawn.GetComponent<WeaponPickup>()) {
                newSpawn.GetComponent<WeaponPickup>().droppedByEnemy = true;
            }
        }
    }
}
