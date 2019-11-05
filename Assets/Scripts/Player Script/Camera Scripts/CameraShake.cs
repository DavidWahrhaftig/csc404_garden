using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{


    private Rewired.Player gamePadController;

    private Animator camAnim;
    private bool shakeLeft, shakeRight;
    public float power = 0f;
    private float gain = 1f;

    public float resistanceMeter = 0;
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

        if (GetComponentInParent<PlayerLogic>().isCaught())
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
        power += gain;
    }

    public void camShakeRight()
    {
        camAnim.SetTrigger("Shake Right");
        power += gain;
    }

    public float getPower()
    {
        return power;
    }

    public void setPower(float value)
    {
        power = value;
    }

    public float getResistanceMeter()
    {
        return resistanceMeter;
    }

    public void setResistanceMeter(float value)
    {
        resistanceMeter = value;
    }
}
