using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    private Vector3 tankSpawnPosition;
    private Vector3 spawnPosition = new Vector3(0, 0, -100);
    private Vector3 rotateTo = new Vector3(0, 0, -10);
    public GameObject tankPrefab;
    public GameObject mortarCrewPrefab;
    private float spawnRate;
    private GameObject left;
    private GameObject right;
    private GameObject mortarCrew;

    // spawn rate
    private float cooldownTime = 10f;

    [SerializeField]
    public float spawnDistance = 120f;
    // Start is called before the first frame update
    void Start()
    {
        // for initial spawn, 10s cooldown is applied afterwards
        spawnRate = -cooldownTime;

        // spawn mortar crew on scene load
        mortarCrew = Instantiate(mortarCrewPrefab, spawnPosition, Quaternion.identity);
        
    }

    void Update()
    {

        // calculate passed time
        spawnRate -= Time.deltaTime;

        // if space is pressed and cooldown time expired, attempt to spawn new tank object
        if(Input.GetKey("space") && spawnRate < 0f) {
            float random = Random.Range(-1f, 1f);

            // make sure tanks are not already instantiated on this side
            if(random > 0 && right == null) {
                tankSpawnPosition = new Vector3(spawnDistance, 0, -10);
                right =  Instantiate(tankPrefab, tankSpawnPosition, Quaternion.Euler(0, -90, 0));
            } else if(random < 0 && left == null) {
                tankSpawnPosition = new Vector3(-spawnDistance, 0, -10);
                left = Instantiate(tankPrefab, tankSpawnPosition, Quaternion.Euler(0, 90, 0));
            }

            // set cooldown time back;
            spawnRate = cooldownTime;

        } else if(Input.GetKeyDown(KeyCode.R)) { // reset scene
            Destroy(left);
            Destroy(right);
            Destroy(mortarCrew);
            mortarCrew = Instantiate(mortarCrewPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
