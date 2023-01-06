using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour {
    // exit application
    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
        }    
    }
}
