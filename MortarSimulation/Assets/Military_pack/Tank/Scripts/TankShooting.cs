using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {

    private AudioSource myTankAudio;
    public ParticleSystem[] gunFx;
    public AudioClip fireSound;
    public Rigidbody tankRoundPrefab;
    public Transform tankBarrelEnd;
    private float cooldown = 5f;

    private float azmuthSlop = 5f;
    private Quaternion originalBarrelEnd;
    private float velocitySlop = 1f;

    // shell speed
    private float shellVelocity = 100000f;

    // get audio component at start
    void Start()
    {
        myTankAudio = GetComponent<AudioSource>();
        originalBarrelEnd = tankBarrelEnd.rotation;
    }

    void Update() {
        cooldown -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.S) && cooldown > 0f) {
            FireTank();
            cooldown = 5f;
        }
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