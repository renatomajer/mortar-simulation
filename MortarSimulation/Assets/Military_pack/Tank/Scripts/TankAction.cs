using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAction : MonoBehaviour {

    public Rigidbody tankBody;
    public GameObject explosionPrefab;

    private Quaternion lookRotation;
    private bool didTurn = false;

    // access script variable
    private TankShooting tankShootingScript;

    private Vector3 targetPosition = new Vector3(0, 0, -100);
    private float moveAfterRotation = 20f;
    private float moveBeforeRotation = 120.0f;
    private float rotationSpeed = 3.0f;
    private float speed = 3.0f;

    // Use this for initialization
	void Start () {
        tankBody = GetComponent<Rigidbody>();
        moveBeforeRotation += Random.Range(-30f, 0f);
        moveAfterRotation += Random.Range(0f, 30f);
        tankShootingScript = this.GetComponentInChildren<TankShooting>();
	}

    // Update is called once per frame
    void Update() {
        if(moveBeforeRotation > 0f) {
            tankBody.velocity = transform.forward * speed;
            moveBeforeRotation -= tankBody.velocity.magnitude * Time.deltaTime;
            return;
        }

        if(moveBeforeRotation <= 0f && !didTurn) {
            turnTank();

            // when they allign, tank did turn and reduce speed
            if(Vector3.Angle(transform.forward, (targetPosition - transform.position)) <= 0.01f) {
                didTurn = true;
                speed *= 0.5f;
            } else {
                didTurn = false;
            }

            return;
        }

        // close the distance, after tank turned
        if(didTurn && moveAfterRotation > 0f) {
            tankBody.velocity = transform.forward * speed;
            moveAfterRotation -= tankBody.velocity.magnitude * Time.deltaTime;
        }

        // allow shooting
        if(didTurn && moveAfterRotation <= 0f && moveBeforeRotation <= 0f) {
            tankShootingScript.originalBarrelEnd = tankShootingScript.tankBarrelEnd.rotation;
            tankShootingScript.canShoot = true;
        }
    }

    void turnTank() {
        Vector3 lookDirection = (targetPosition - transform.position).normalized;

        lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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
