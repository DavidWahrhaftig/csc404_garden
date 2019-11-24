using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLossProp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float destoryTimer = 2.5f;
    //[SerializeField] AudioClip loseFruitSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("destroyFruitProp", destoryTimer);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            audioSource.Play();
        }
    }

    void destroyFruitProp()
    {
        Destroy(gameObject);
    }
}
