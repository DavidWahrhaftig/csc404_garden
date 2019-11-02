using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{


    private Rewired.Player gamePadController;

    private Animator camAnim;
    private bool shakeLeft, shakeRight;

    // Start is called before the first frame update
    void Start()
    {
        camAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gamePadController = GetComponentInParent<PlayerController>().getGamePadController();
        shakeLeft = gamePadController.GetButtonDown("ShakeLeft");
        shakeRight = gamePadController.GetButtonDown("ShakeRight");

        if (GetComponentInParent<PlayerLogic>().getIsCaught())
        {
            if (shakeLeft)
            {
                camShakeLeft();

            }
            else if (shakeRight)
            {
                camShakeRight();
            }
        }
    }

    public void camShakeLeft()
    {
        camAnim.SetTrigger("Shake Left");
    }

    public void camShakeRight()
    {
        camAnim.SetTrigger("Shake Right");
    }
}
