using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public GameObject tankPrefab;
    public GameObject mortarCrewPrefab;
    private GameObject left;
    private GameObject right;
    private GameObject mortarCrew;
    private Vector3 spawnPosition = new Vector3(0, 0, -100);

    private float cooldownTime = 10f;
    private float spawnRate;
    private bool spawnContinuously = false;
    private float spawnDistance = 120f;
    public bool autoShooting = false;
    
    void Start() {
        spawnRate = -cooldownTime;
        mortarCrew = Instantiate(mortarCrewPrefab, spawnPosition, Quaternion.identity);
    }

    void Update() {
        spawnRate -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.C)) spawnContinuously = true;
        else if(Input.GetKeyDown(KeyCode.S)) spawnContinuously = false;
        else if(Input.GetKeyDown(KeyCode.A)) autoShooting = true;
        else if(Input.GetKeyDown(KeyCode.M)) autoShooting = false;
        else if((Input.GetKey("space") || spawnContinuously) && spawnRate < 0f) SpawnEnemy();
        else if(Input.GetKeyDown(KeyCode.R)) DestroyAll();
    }

    public void DestroyAll() {
        Destroy(left);
        Destroy(right);
        Destroy(mortarCrew);
        mortarCrew = Instantiate(mortarCrewPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnEnemy() {
        float random = Random.Range(-1f, 1f);

        if(random > 0f && right == null) right = Instantiate(tankPrefab, new Vector3(spawnDistance, 0, -10), Quaternion.Euler(0, -90, 0));
        else if(random <= 0f && left == null) left = Instantiate(tankPrefab, new Vector3(-spawnDistance, 0, -10), Quaternion.Euler(0, 90, 0));
        else if(right == null) right = Instantiate(tankPrefab, new Vector3(spawnDistance, 0, -10), Quaternion.Euler(0, -90, 0));
        else if(left == null) left = Instantiate(tankPrefab, new Vector3(-spawnDistance, 0, -10), Quaternion.Euler(0, 90, 0));

        spawnRate = cooldownTime;
    }
}
