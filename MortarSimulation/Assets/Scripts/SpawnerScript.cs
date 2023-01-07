using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    private Vector3 tankSpawnPosition;
    private Vector3 rotateTo = new Vector3(0, 0, -10);
    public GameObject tankPrefab;
    public GameObject mortarCrewPrefab;
    private GameObject left;
    private GameObject right;
    private GameObject mortarCrew;

    // crew spawn position
    private Vector3 spawnPosition = new Vector3(0, 0, -100);

    // spawn cooldown
    private float cooldownTime = 10f;
    private float spawnRate;

    // spawn method
    private bool spawnContinuously = false;

    private float spawnDistance = 120f;

    public bool autoShooting = false;
    
    void Start() {
        // for initial spawn, 10s cooldown is applied afterwards
        spawnRate = -cooldownTime;
        mortarCrew = Instantiate(mortarCrewPrefab, spawnPosition, Quaternion.identity);
    }

    void Update() {
        spawnRate -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.C)) {
            spawnContinuously = true;
        } else if(Input.GetKeyDown(KeyCode.S)) {
            spawnContinuously = false;
        } else if(Input.GetKeyDown(KeyCode.A)) {
            autoShooting = true;
        } else if(Input.GetKeyDown(KeyCode.M)) { 
            autoShooting = false;
        } else if((Input.GetKey("space") || spawnContinuously) && spawnRate < 0f) {
            float random = Random.Range(-1f, 1f); // attempt to spawn tanks randomly

            if(random > 0f && right == null) { // spawn on right side
                tankSpawnPosition = new Vector3(spawnDistance, 0, -10);
                right =  Instantiate(tankPrefab, tankSpawnPosition, Quaternion.Euler(0, -90, 0));
            } else if(random <= 0f && left == null) { // spawn on left side
                tankSpawnPosition = new Vector3(-spawnDistance, 0, -10);
                left = Instantiate(tankPrefab, tankSpawnPosition, Quaternion.Euler(0, 90, 0));
            } else if(right == null) { // if random was left and tank on left exists, spawn right
                tankSpawnPosition = new Vector3(spawnDistance, 0, -10);
                right =  Instantiate(tankPrefab, tankSpawnPosition, Quaternion.Euler(0, -90, 0));
            } else if(left == null) { // if random was right and tank on right exists, spawn left
                tankSpawnPosition = new Vector3(-spawnDistance, 0, -10);
                left = Instantiate(tankPrefab, tankSpawnPosition, Quaternion.Euler(0, 90, 0));
            }

            spawnRate = cooldownTime; // reset cooldown time

        } else if(Input.GetKeyDown(KeyCode.R)) { // reset scene
            Destroy(left);
            Destroy(right);
            Destroy(mortarCrew);
            mortarCrew = Instantiate(mortarCrewPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
