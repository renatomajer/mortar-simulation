using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAction : MonoBehaviour
{

    public Rigidbody tankBody;
    public GameObject explosionPrefab;

    public Quaternion lookRotation;
    public bool didTurn = false;

    private TankShooting tankShootingScript;

    [SerializeField]
    private Vector3 targetPosition = new Vector3(0, 0, -100);

    [SerializeField]
    public float moveAfterRotation = 20f;

    [SerializeField]
    private float range = 120.0f;

    [SerializeField]
    private float rotationSpeed = 3.0f;

    [SerializeField]
    private float speed = 3.0f;

    // Use this for initialization
	void Start () {
        tankBody = GetComponent<Rigidbody>();
        range += Random.Range(-30f, 0f);
        moveAfterRotation += Random.Range(-5f, 5f);
        tankShootingScript = this.GetComponentInChildren<TankShooting>();
	}

    // Update is called once per frame
    void Update()
    {
        // if range is > 0 and tank didn't turn yet, move tank forward and substract passed distance
        if(range > 0f) {
            tankBody.velocity = transform.forward * speed;
            range -= tankBody.velocity.magnitude * Time.deltaTime;
            return;
        }

        // if range is < 0 and tank did turn, turn the tank towards the crew and wait until they allign
        if(range <= 0f && !didTurn) {
            turnTank();

            // if they allign, tank did turn and reduce speed
            if(Vector3.Angle(transform.forward, (targetPosition - transform.position)) <= 0.01f) {
                didTurn = true;
                speed *= 0.5f;
            }

            return;
        }

        // if tank turned towards the crew, close the distance
        if(didTurn && moveAfterRotation > 0f) {
            tankBody.velocity = transform.forward * speed;
            moveAfterRotation -= tankBody.velocity.magnitude * Time.deltaTime;
        }

        // allow shooting
        if(didTurn && moveAfterRotation <= 0f && range <= 0f) {
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
        if(collision.gameObject.CompareTag("Shell")) {
            Debug.Log("Hit");
            Invoke("tankExplosion", 1.0f);
        } else if(collision.gameObject.CompareTag("Enviroment")) {
            Destroy(collision.gameObject);
        }
    
    }
}
