using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooting : MonoBehaviour {

    private AudioSource myTankAudio;
    public ParticleSystem[] gunFx;
    public AudioClip fireSound;
    public Rigidbody tankRoundPrefab;
    public Transform tankBarrelEnd;
    private Quaternion originalBarrelEnd;
    private SpawnerScript spawnerScript;
    private GameObject crew;
    public bool canShoot = false;
    private float cooldown;
    private float azmuthSlop = 3f;
    private float cooldownRate = 5f;

    // shell speed
    private float shellVelocity = 1000000f;

    // get audio component at start
    void Start() {
        spawnerScript = GameObject.FindWithTag("GameController").GetComponent<SpawnerScript>();
        myTankAudio = GetComponent<AudioSource>();
        crew = GameObject.FindWithTag("Player");
        cooldown = cooldownRate + Random.Range(-2f, 2f);
    }

    void Update() {
        
        originalBarrelEnd = tankBarrelEnd.rotation;
        
        cooldown -= Time.deltaTime;
        
        if((Input.GetKeyDown(KeyCode.T) || spawnerScript.autoShooting) && cooldown < 0f && canShoot) {
            FireTank();
            cooldown = cooldownRate + Random.Range(-2f, 2f);
        }
    }

    public void FireTank() {
        myTankAudio.clip = fireSound;
        myTankAudio.loop = false;
        myTankAudio.Play();
        foreach (ParticleSystem fire in gunFx) {
            fire.Play();
        }

        ShootTankRound();
    }

    public void ShootTankRound() {
        Quaternion slop = Quaternion.Euler(Random.Range(-azmuthSlop, azmuthSlop), Random.Range(-azmuthSlop, azmuthSlop), 0);
        
        Transform tmp = tankBarrelEnd;
        if(crew != null) {
            tmp.LookAt(crew.transform);
        } else {
            tmp = tankBarrelEnd;
        }
        
        tmp.rotation = tankBarrelEnd.rotation * slop;
        Rigidbody emptyShellInstance = Instantiate(tankRoundPrefab, tmp.position, (tmp.rotation)) as Rigidbody;
        emptyShellInstance.AddForce(tankBarrelEnd.forward * shellVelocity);
    }
}