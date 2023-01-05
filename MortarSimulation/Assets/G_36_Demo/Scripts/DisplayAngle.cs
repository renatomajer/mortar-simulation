using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayAngle : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textMesh;

    public GameObject mortarBarrel;
    
    void Update()
    {
        mortarBarrel = GameObject.FindWithTag("BarrelEnd");
        if(mortarBarrel != null) {
            textMesh.text = "Angle: " + System.Math.Round(360 - mortarBarrel.transform.rotation.eulerAngles.x, 2);
        }
    }
}
