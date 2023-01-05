using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewCollider : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Shell")) {
            Debug.Log("Dead");
            Destroy(gameObject);
        }    
    }
}
