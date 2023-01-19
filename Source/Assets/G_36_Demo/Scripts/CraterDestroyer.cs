using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterDestroyer : MonoBehaviour {

    private float lifeTime = 10f;

    // destroy crater objects after lifeTime seconds
    void Update() {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f) {
            Destroy(gameObject);
        }
    }
}
