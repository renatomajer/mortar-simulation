using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraterDestroyer : MonoBehaviour
{

    private float lifeTime = 10f;

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f) {
            Destroy(gameObject);
        }
    }
}
