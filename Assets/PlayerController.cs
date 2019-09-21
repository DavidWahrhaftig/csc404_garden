using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movingSpeed = 20;
    public float rotationSpeed = 100;

    int numFruit = 0;

    FruitBushScript fruitBushScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject fruitBush = GameObject.Find("FruitBush");
        fruitBushScript = fruitBush.GetComponent<FruitBushScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "FruitBush")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            numFruit += fruitBushScript.fruitsInBush;
            fruitBushScript.fruitsInBush = 0;
        }
    }
}

