using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    /***
    #region Controller Inputs
    float moveVertical;
    float moveHorizontal;
    float rotateHorizontal;
    bool jumpButton;
    bool runButton;
    bool isCameraFlipped;
    int flip = 1;
    #endregion ***/

    public Transform player1;
    public Transform player2;
    public Transform witch;



    private void Update()
    {
        /***
        moveVertical = gamePadController.GetAxis("Move Vertical");
        moveHorizontal = gamePadController.GetAxis("Move Horizontal");
        rotateHorizontal = gamePadController.GetAxis("Rotate");
        jumpButton = gamePadController.GetButtonDown("Jump");
        runButton = gamePadController.GetButton("Run");
        isCameraFlipped = gamePadController.GetButtonDown("Camera Flip");
        ****/

        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }


        }
        if (popUpIndex == 0) /**move**/
        {
            bool player1Moved = false;
            bool player2Moved = false;

            if (player1.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Move Vertical"))
            {
                player1Moved = true;
            }

            if (player2.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Move Vertical"))
            {
                player2Moved = true;
            }

            if (player1Moved && player2Moved)/**both players have moved**/
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 1) /***jump***/
        {

            bool player1Jumped = false;
            bool player2Jumped = false;

            if (player1.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Move Vertical"))
            {
                player1Jumped = true;
            }

            if (player2.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Move Vertical"))
            {
                player2Jumped = true;
            }

            if (player1Jumped && player2Jumped)/**both players have jumped**/
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 2) /**flip camera**/
        {
            bool player1CamFlip = false;
            bool player2CamFlip = false;

            if (player1.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Camera Flip"))
            {
                player1CamFlip = true;
            }

            if (player2.GetComponent<PlayerController>().getGamePadController().GetButtonDown("Camera Flip"))
            {
                player2CamFlip = true;
            }

            if (player1CamFlip && player2CamFlip)/**both players have flipped camera**/
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 3)
        {
            if (false)/**get fruit**/
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 4)
        {
            if (false)/**throw light ball**/
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 5)
        {
            if (false)/**shake off the witch**/
            {
                popUpIndex++;
            }
        }

    }
}
