using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightOrbP1 : MonoBehaviour
{

    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    public float reloadTime = 15;
    private float spawnTimer;
    private bool charge = true;
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
        spawnTimer = reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (charge)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                CreateEffect();
                charge = false;
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
