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

    public float cooldown;

    public float azmuthSlop = 3f;
    public Quaternion originalBarrelEnd;
    public float velocitySlop = 1f;

    [SerializeField]
    private float cooldownRate = 5f;

    [SerializeField]
    private bool autoShooting = false;

    // shell speed
    public float shellVelocity = 100000f;

    // get audio component at start
    void Start()
    {
        myTankAudio = GetComponent<AudioSource>();
        cooldown = cooldownRate;
    }

    void Update() {
        cooldown -= Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.A)) {
            autoShooting = true;
        } else if(Input.GetKeyDown(KeyCode.M)) {
            autoShooting = false;
        } else if((Input.GetKeyDown(KeyCode.T) || autoShooting) && cooldown < 0f && canShoot) {
            FireTank();
            cooldown = cooldownRate;
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