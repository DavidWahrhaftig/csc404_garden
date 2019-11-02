using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class SpawnLightOrb : MonoBehaviour
{
    public AudioClip shotSound;
    private AudioSource audioSource;
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;

    /*
    public float reloadTime = 15;
    private float spawnTimer;
    private bool charged = true;
    public int ammoSize;
    public int ammo;
    */

    // Magic ORB Meter
    [Range(0,100)]
    public float magicCharge = 100f; // start at 100 percent
    [Range(0,100)]
    public float magicShotPower = 20f; // percent usage of shot
    public float reloadSpeed = 10f; //  the time it takes to load the charge from 0% to 100%

    private PlayerLogic playerLogic;
    private PlayerController playerController;

    private Rewired.Player gamePadController;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
        //spawnTimer = reloadTime;
        playerLogic = GetComponent<PlayerLogic>();
        playerController = GetComponent<PlayerController>();
        //ammo = ammoSize;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gamePadController = playerController.getGamePadController(); // have to reference in Update method, else gamePadController is null

        bool shoot = gamePadController.GetButtonDown("Shoot");

        /*
        if (charged)
        {
            if (shoot && !playerLogic.getIsGlowing()) // only shoot when there is a charge and the player is not glowing
            {
                if (!audioSource.isPlaying) // so it doesn't layer
                {
                    audioSource.PlayOneShot(shotSound);
                }
                CreateEffect();
                ammo -= 1;

                charged &= ammo > 0;
                
            } 
            else if (shoot && playerLogic.getIsGlowing())
            {
                //TODO: give feedback to player to tell them they cannot shoot while they are glowing
            }
        }
        else
        {
            spawnTimer -= Time.smoothDeltaTime;
            if (spawnTimer < 0)
            {
                charged = true;
                ammo = ammoSize;
                spawnTimer = reloadTime;
            }
        }
        */

        if (magicCharge >= magicShotPower)
        {
            if (shoot && !playerLogic.isGlowing()) // only shoot when there is a charge and the player is not glowing
            {
                audioSource.PlayOneShot(shotSound);
                CreateEffect();
                magicCharge -= magicShotPower; // decrease charge
            }
        }

        if (magicCharge < 100)
        {
            magicCharge += Time.deltaTime / reloadSpeed * 100;
        }
    }

    void CreateEffect()
    {
        GameObject visualEffect;

        if (firePoint != null)
        {
            visualEffect = Instantiate(effectToSpawn, firePoint.transform.position,
                Quaternion.identity);
            visualEffect.transform.rotation = firePoint.transform.rotation;
        }
        else
        {
            Debug.Log("Null Fire Point");
        }
    }

    /*
    public float getSpawnTimer()
    {
        return spawnTimer;
    }

    public float getReloadTime()
    {
        return reloadTime;
    }

    public int getAmmo()
    {
        return ammo;
    }
    */
}
