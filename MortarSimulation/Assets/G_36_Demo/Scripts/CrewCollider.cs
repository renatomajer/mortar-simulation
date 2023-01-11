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
}
