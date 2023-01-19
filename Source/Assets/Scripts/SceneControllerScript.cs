using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerScript : MonoBehaviour {

    // hide cursor
    void Start() {
        Cursor.visible = false;
    }

    // exit application
    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            Application.Quit();
        }    
    }
}
