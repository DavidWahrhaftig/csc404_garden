using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class SpawnLightOrb : MonoBehaviour
{
    public AudioClip shotSound;
    
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;

    // Magic ORB Meter
    [Range(0,100)]
    public float magicCharge = 100f; // start at 100 percent
    [Range(0,100)]
    public float magicShotPower = 20f; // percent usage of shot
    public float reloadSpeed = 10f; //  the time it takes to load the charge from 0% to 100%

    private AudioSource audioSource;
    private PlayerLogic playerLogic;
    private PlayerController playerController;
    private Rewired.Player gamePadController;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
        playerLogic = GetComponent<PlayerLogic>();
        playerController = GetComponent<PlayerController>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gamePadController = playerController.getGamePadController(); // have to reference in Update method, else gamePadController is null

        bool shoot = gamePadController.GetButtonDown("Shoot");


        if (magicCharge >= magicShotPower)
        {
            if (shoot && !playerLogic.isGlowing() && !playerLogic.isDisabled()) // only shoot when there is a charge and the player is not glowing
            {
                //audioSource.PlayOneShot(shotSound);
                //CreateEffect();
                //magicCharge -= magicShotPower; // decrease charge
                GetComponent<Animator>().SetTrigger("isShooting");
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

    public void shoot(float delay)
    {
        Invoke("shootLater", delay);
        
    }

    void shootLater()
    {
        audioSource.PlayOneShot(shotSound);
        CreateEffect();
        magicCharge -= magicShotPower; // decrease charge

        if (magicCharge <= Mathf.Epsilon)
        {
            magicCharge = 0f;
        }
    }
}
