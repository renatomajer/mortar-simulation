using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    private Vector3 spawnPosition;
    private Vector3 rotateTo = new Vector3(0, 0, -10);
    public GameObject tankPrefab;
    private float spawnRate;

    [SerializeField]
    public float spawnDistance = 120f;
    // Start is called before the first frame update
    void Start()
    {
        spawnRate = -10f;
    }

    // Update is called once per frame
    void Update()
    {

        spawnRate -= Time.deltaTime;

        if(Input.GetKey("space") && spawnRate < 0f) {
            float random = Random.Range(-1f, 1f);

            if(random > 0) {
                spawnPosition = new Vector3(spawnDistance, 0, -10);
                Instantiate(tankPrefab, spawnPosition, Quaternion.Euler(0, -90, 0));
            } else {
                spawnPosition = new Vector3(-spawnDistance, 0, -10);
                Instantiate(tankPrefab, spawnPosition, Quaternion.Euler(0, 90, 0));
            }

            spawnRate = 10f;
        }
    }
}
