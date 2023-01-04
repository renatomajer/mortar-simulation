using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAction : MonoBehaviour {
    private AudioSource myAudio;
    public ParticleSystem[] gunFx;
    public AudioClip fireSound;
    public Rigidbody mortarPrefab;
    public Transform mortarBarrelEnd;

    // shell speed
    private float shellVelocity = 22000f;

    // Use this for initialization
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

   


    public void Fire()
    {
       
        myAudio.clip = fireSound;
        myAudio.loop = false;
        myAudio.Play();
        foreach (ParticleSystem fire in gunFx)
        {
            fire.Play();

        }

        ShootMortarRound();
    }

    public void ShootMortarRound()
    {
        mortarBarrelEnd.rotation = mortarBarrelEnd.rotation;
        Rigidbody emptyShellInstance = Instantiate(mortarPrefab, mortarBarrelEnd.position, (mortarBarrelEnd.rotation)) as Rigidbody;
        emptyShellInstance.AddForce(mortarBarrelEnd.forward * shellVelocity);
    }


   
}
