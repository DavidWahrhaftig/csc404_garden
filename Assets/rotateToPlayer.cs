using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateToPlayer : MonoBehaviour
{
    public Transform t;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = t.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }
}
