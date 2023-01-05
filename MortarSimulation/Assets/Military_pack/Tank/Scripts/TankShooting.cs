using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {
    private AudioSource myTankAudio;
    public ParticleSystem[] gunFx;
    public AudioClip fireSound;
    public Rigidbody tankRoundPrefab;
    public Transform tankBarrelEnd;

    public float azmuthSlop = 10f;
    private Quaternion originalBarrelEnd;
    public float velocitySlop = 1f;

    // shell speed
    private float shellVelocity = 22000f;

    // Use this for initialization
    void Start()
    {
        myTankAudio = GetComponent<AudioSource>();
        FireTank();
    }

   


    public void FireTank()
    {
       
        myTankAudio.clip = fireSound;
        myTankAudio.loop = false;
        myTankAudio.Play();
        foreach (ParticleSystem fire in gunFx)
        {
            fire.Play();

        }

        ShootTankRound();
    }

    public void ShootTankRound()
    {

        shellVelocity = shellVelocity + (Random.Range(-velocitySlop, velocitySlop));
        float randomSlop = Random.Range(-azmuthSlop, azmuthSlop);
        Quaternion slop = Quaternion.Euler(randomSlop, Random.Range(-azmuthSlop, azmuthSlop), 0);
        tankBarrelEnd.rotation = tankBarrelEnd.rotation * slop;

        Rigidbody emptyShellInstance = Instantiate(tankRoundPrefab, tankBarrelEnd.position, (tankBarrelEnd.rotation)) as Rigidbody;
        emptyShellInstance.AddForce(tankBarrelEnd.forward * shellVelocity);

        tankBarrelEnd.rotation = originalBarrelEnd;
    }
}