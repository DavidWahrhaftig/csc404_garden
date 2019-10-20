using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropelLightOrb : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject impactPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (impactPrefab != null)
        {
            var impactVFX = Instantiate(impactPrefab, pos, rotation);
            var psHit = impactVFX.GetComponent<ParticleSystem>();

            if (psHit != null)
            {
                Destroy(impactVFX, psHit.main.duration);
            }

            else
            {
                var psChild = impactVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(impactVFX, psChild.main.duration);
            }
        }

        Destroy(gameObject);
    }
}
