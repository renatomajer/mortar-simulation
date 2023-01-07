using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrew : MonoBehaviour {

    private float sensitivity = 0.1f;

    // rotate mortar crew using arrow keys
    void Update() {
        if (Input.GetKey("right")) {
            transform.Rotate(0, sensitivity, 0);
        } else if (Input.GetKey("left")) {
            transform.Rotate(0, -sensitivity, 0);
        }
    }
}
