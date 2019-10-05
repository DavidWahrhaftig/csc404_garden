using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCast : MonoBehaviour
{
    public GameObject currentHitObject; // for debugging
    public float sphereRadius; // for debugging
    
    public LayerMask layerMask;

    public float angle;
    public float currentHitDistance; // for debugging
    public float distance1;
    public Transform targetPlayer;
   
    private Vector3 origin; // position of this gameObject
    private Vector3 direction; // the direction to shoot raycast
    private Transform parent;
    private Oscillator oscillator; // access to gameObject's floating functionality
    private Light lightComponent;
    private Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        oscillator = GetComponent<Oscillator>();
        lightComponent = GetComponent<Light>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = Vector3.down;
        angle = lightComponent.spotAngle;
        RaycastHit hit1; // only used for sphere radius calculation
        RaycastHit hit2;
        Ray landingRay = new Ray(transform.position, Vector3.down);


        if (Physics.Raycast(landingRay, out hit1))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit1.distance);
            // calculate the spehre radius
            // opposite = adjacent * tan(angle)
            // radius = distance to collider * tan(spotlight angle / 2) --> using half the angle to obtain a right angle
            sphereRadius = hit1.distance * Mathf.Tan(Mathf.Deg2Rad * (this.lightComponent.spotAngle/2));
            distance1 = hit1.distance;
            if (Physics.SphereCast(origin, sphereRadius, direction, out hit2, hit1.distance, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                currentHitObject = hit2.transform.gameObject;
                currentHitDistance = hit2.distance + sphereRadius; //to use sphere as a semisphere

                if (hit2.collider.tag == "Player")
                {
                    //print("Ray Hit Player");
                    targetPlayer = hit2.collider.transform;
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isChasing", false);
                    animator.SetBool("isPatrolling", false);
                    // if (player.isHidden) {animator.SetBool("isPatrolling", true); animator.SetBool("isCapturing", false);}
                    //else {
                    animator.SetBool("isCapturing", true);
                }

                if (Input.GetKeyDown(KeyCode.Space)) // to indicate a player is hidden
                {
                    animator.SetBool("isIdle", false);
                    animator.SetBool("isChasing", false);
                    animator.SetBool("isPatrolling", true);
                    animator.SetBool("isCapturing", false);
                }
            }           
        } 
    }
    private void OnDrawGizmos()
    {
        /*
         * Displays SphereCast Visually for Debuging purposes.
         * Can only be seen in the Scene window while game is running
         */

        Gizmos.color = Color.red;
        //Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
