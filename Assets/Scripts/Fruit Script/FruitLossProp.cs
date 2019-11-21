using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitLossProp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float destoryTimer = 2.5f;

    void Start()
    {
        Invoke("destroyFruitProp", destoryTimer);
    }

    void destroyFruitProp()
    {
        Destroy(gameObject);
    }
}
