using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {

    private AudioSource myTankAudio;
    public ParticleSystem[] gunFx;
    public AudioClip fireSound;
    public Rigidbody tankRoundPrefab;
    public Transform tankBarrelEnd;

    public bool canShoot = false;

    private float cooldown;
    public float azmuthSlop = 0.025f;

    public Quaternion originalBarrelEnd;

    [SerializeField]
    private float cooldownRate = 5f;

    [SerializeField]
    private bool autoShooting = false;

    // shell speed
    private float shellVelocity = 100000f;

    // get audio component at start
    void Start() {
        myTankAudio = GetComponent<AudioSource>();
        cooldown = cooldownRate + Random.Range(-2f, 2f);
    }

    void Update() {
        cooldown -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.A)) {
            autoShooting = true;
        } else if(Input.GetKeyDown(KeyCode.M)) {
            autoShooting = false;
        } else if((Input.GetKeyDown(KeyCode.T) || autoShooting) && cooldown < 0f && canShoot) {
            FireTank();
            cooldown = cooldownRate + Random.Range(-2f, 2f);
        }
    }

    public void FireTank() {
        myTankAudio.clip = fireSound;
        myTankAudio.loop = false;
        myTankAudio.Play();
        foreach (ParticleSystem fire in gunFx)
        {
            fire.Play();
        }

        ShootTankRound();
    }

    public void ShootTankRound() {
        Quaternion slop = Quaternion.Euler(Random.Range(-azmuthSlop, azmuthSlop), Random.Range(-azmuthSlop, azmuthSlop), 0);

        tankBarrelEnd.rotation = tankBarrelEnd.rotation * slop;
        Rigidbody emptyShellInstance = Instantiate(tankRoundPrefab, tankBarrelEnd.position, (tankBarrelEnd.rotation)) as Rigidbody;
        emptyShellInstance.AddForce(tankBarrelEnd.forward * shellVelocity);

        tankBarrelEnd.rotation = originalBarrelEnd;
    }
}