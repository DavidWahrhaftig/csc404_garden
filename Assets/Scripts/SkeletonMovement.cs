using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{

    public Transform[] waypoints;
    public float speed;

    private int currentTarget;

    // Update is called once per frame
    void Update()
    {
        if (transform.position != waypoints[currentTarget].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position,
                waypoints[currentTarget].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            currentTarget = (currentTarget + 1) % waypoints.Length;
        }
    }
}
