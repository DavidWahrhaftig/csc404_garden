using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 20f;

    // Game Pad related fields
    private Rewired.Player gamePadController;
    private float lookVertical;
    private bool cameraFlip;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gamePadController = GetComponentInParent<PlayerController>().getGamePadController();

        lookVertical = gamePadController.GetAxis("Look Vertical");
        cameraFlip = gamePadController.GetButtonDown("Camera Flip");
        

        transform.Rotate(rotationSpeed * lookVertical * Time.deltaTime, 0, 0); // camera vertical rotation
        
        
        if (cameraFlip)
        {
            // move camera in front/back of player
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z * -1f);
            
            // flip camera
            transform.Rotate(0f, 180, 0f, Space.World);
            
            // inverse the horizontal movment and horizontal rotation in PlayerController.cs
        }          
    }
}
