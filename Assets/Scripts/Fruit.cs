using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        other.GetComponent<PlayerController>().incrementFruitCounter();
        GameManager gameManager = FindObjectOfType<GameManager>();
        //Invoke("destoryFruit", 0);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void destroyFruit()
    {

        Destroy(gameObject);
    }
        
}
