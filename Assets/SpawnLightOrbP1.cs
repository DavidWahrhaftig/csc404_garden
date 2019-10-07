using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLightOrbP1 : MonoBehaviour
{

    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject effectToSpawn;
    private float spawnTime = 15;
    private bool charge = true;
    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx[0];
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
            spawnTime -= Time.smoothDeltaTime;
            if (spawnTime < 0)
            {
                charge = true;
                spawnTime = 15;
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
}
