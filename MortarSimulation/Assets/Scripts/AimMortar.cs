using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimMortar : MonoBehaviour
{
    
    [SerializeField]
    private float sensitivity = 0.1f;

    [SerializeField]
    public TextMeshProUGUI text;

    void start() {
        text.text = "Angle " + System.Math.Round(360 - transform.rotation.eulerAngles.x, 2);
    }

    // rotates in range from 280 to 330 degrees
    void Update()
    {
        if(Input.GetKey ("up")) {
            if(transform.rotation.eulerAngles.x >= 280) {
                Quaternion step = Quaternion.Euler(-sensitivity, 0, 0);
                transform.rotation = transform.rotation * step;
                text.text = "Angle " + System.Math.Round(360 - transform.rotation.eulerAngles.x, 2);
            }
        } else if (Input.GetKey ("down")) {
            if(transform.rotation.eulerAngles.x <= 330) {
                Quaternion step = Quaternion.Euler(sensitivity, 0, 0);
                transform.rotation = transform.rotation * step;
                text.text = "Angle " + System.Math.Round(360 - transform.rotation.eulerAngles.x, 2);
            }
        }
    }
}
