using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShell : MonoBehaviour {

    private Rigidbody mortarRigidbody;
    public GameObject explosionPrefab;
    public GameObject explosionNoCrater;
    
	void Start () {
        mortarRigidbody = GetComponent<Rigidbody>();
	}
	
	void Update () {
        this.transform.forward = Vector3.Slerp(this.transform.forward, mortarRigidbody.velocity.normalized, Time.deltaTime);
        if(transform.position.y < -1f) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
        GameObject player = GameObject.FindWithTag("Player");
        
        if(player != null)
            if(player.GetComponent<CrewCollider>().canDie)
                if(Vector3.Distance(gameObject.transform.position, player.transform.position) <= 5f)
                    Destroy(player);

        Destroy(gameObject);
    }
}
