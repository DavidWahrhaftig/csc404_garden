using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightOrb : MonoBehaviour
{
    public AudioClip shotSound;
    private AudioSource audioSource;
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    public float reloadTime = 15;
    private float spawnTimer;
    private bool charge = true;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
        spawnTimer = reloadTime;
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (charge)
        {
            if (Input.GetButtonDown("Shoot" + playerController.gamePad) && !playerController.getIsGlowing()) // only shoot when there is a charge and the player is not glowing
            {
                if (!audioSource.isPlaying) // so it doesn't layer
                {
                    audioSource.PlayOneShot(shotSound);
                }
                CreateEffect();
                charge = false;
            } 
            else if (Input.GetButtonDown("Shoot" + playerController.gamePad) && playerController.getIsGlowing())
            {
                //TODO: give feedback to player to tell them they cannot shoot while they are glowing
            }
        }
        else
        {
            spawnTimer -= Time.smoothDeltaTime;
            if (spawnTimer < 0)
            {
                charge = true;
                spawnTimer = reloadTime;
            }
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

    public float getSpawnTimer()
    {
        return spawnTimer;
    }

    public float getReloadTime()
    {
        return reloadTime;
    }
}
