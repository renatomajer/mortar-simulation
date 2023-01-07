using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAction : MonoBehaviour {

    public Rigidbody tankBody;
    public GameObject explosionPrefab;

    // access script variable
    private TankShooting tankShootingScript;

    private float moveAfterRotation = 20f;
    private float moveBeforeRotation = 120.0f;
    private float rotationSpeed = 3.0f;
    private float speed = 3.0f;
    private GameObject crew;


    // Use this for initialization
	void Start () {
        tankBody = GetComponent<Rigidbody>();
        moveBeforeRotation += Random.Range(-30f, 0f);
        moveAfterRotation += Random.Range(0f, 30f);
        tankShootingScript = this.GetComponentInChildren<TankShooting>();
        crew = GameObject.FindWithTag("Player");
	}

    // Update is called once per frame
    void Update() {
        if(moveBeforeRotation > 0f) {
            tankBody.velocity = transform.forward * speed;
            moveBeforeRotation -= tankBody.velocity.magnitude * Time.deltaTime;
            return;
        }

        turnTank();

        if(moveAfterRotation > 0f) {
            tankBody.velocity = transform.forward * speed;
            moveAfterRotation -= tankBody.velocity.magnitude * Time.deltaTime;
            return;
        }

        // allow shooting
        if(moveAfterRotation <= 0f) {
            GameObject crew = GameObject.FindWithTag("Player");
            if(crew == null) {
                tankShootingScript.canShoot = false;
            } else {
                tankShootingScript.tankBarrelEnd.transform.LookAt(crew.transform);
                tankShootingScript.canShoot = true;
            }
        }
    }

    void turnTank() {
        if(crew == null) {
            return;
        } else {
            Vector3 lookDirection = (crew.transform.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void tankExplosion() {
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Shell")) { // invoke tank explosion
            Debug.Log("Hit");
            Invoke("tankExplosion", 1.0f);
        } else if(collision.gameObject.CompareTag("Enviroment")) { // destroy enviroment objects
            Destroy(collision.gameObject);
        }
    }
}
