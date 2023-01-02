using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAction : MonoBehaviour
{

    private Rigidbody tankBody;
    public GameObject explosionPrefab;
    // Use this for initialization
	void Start () {
        tankBody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void tankExplosion() {
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log("Hit");
        if(collision.gameObject.CompareTag("Shell")) {
            Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            Invoke("tankExplosion", 1.0f);
        }
    
    }
}
