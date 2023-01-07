using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMortar : MonoBehaviour {
    
    private float sensitivity = 0.1f;

    // rotates in range from 30 to 80 degrees
    void Update() {
        if(Input.GetKey ("up")) {
            if(transform.rotation.eulerAngles.x >= 280) {
                Quaternion step = Quaternion.Euler(-sensitivity, 0, 0);
                transform.rotation = transform.rotation * step;
            }
        } else if (Input.GetKey ("down")) {
            if(transform.rotation.eulerAngles.x <= 330) {
                Quaternion step = Quaternion.Euler(sensitivity, 0, 0);
                transform.rotation = transform.rotation * step;
            }
        }
    }
}
