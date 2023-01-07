using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewCollider : MonoBehaviour {

    private bool canDie = false;

    void Update() {
        if(Input.GetKeyDown(KeyCode.E)) {
            canDie = true;
        } else if(Input.GetKeyDown(KeyCode.D)) {
            canDie = false;
        }
    }

    // destroy mortar crew on tank hit
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Shell") && canDie) {
            Debug.Log("Dead");
            Destroy(gameObject);
        }    
    }
}
