using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewCollider : MonoBehaviour {

    public bool canDie = false;

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            canDie = true;
        } else if(Input.GetKeyDown(KeyCode.D)) {
            canDie = false;
        }
    }

    // destroy mortar crew on tank hit
    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Shell") && canDie) {
            Debug.Log("Dead");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }    
    }
}
