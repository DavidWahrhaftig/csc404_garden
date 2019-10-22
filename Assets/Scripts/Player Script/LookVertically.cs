using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class LookVertically : MonoBehaviour
{
    private Quaternion originalRotation;
    public float rotationSpeed = 20f;
    private Rewired.Player gamePadController;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        //gamePadController = GetComponentInParent<PlayerController>().getGamePadController();
    }

    // Update is called once per frame
    void Update()
    {
        gamePadController = GetComponentInParent<PlayerController>().getGamePadController();
        float lookVertical = gamePadController.GetAxis("Look Vertical");
        transform.Rotate(rotationSpeed * lookVertical * Time.deltaTime, 0, 0);
    }
}
