using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++) {
            if (i == popUpIndex)
                {
                    popUps[popUpIndex].SetActive(true);
                }
                else
                {
                    popUps[popUpIndex].SetActive(false);
                }


        }
        if (popUpIndex == 0)
        {
            if (false)/**left and right movement**/
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 1)
        {
            if (false)/**jump**/
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 2)
        {
            if (false)/**rotate camera**/
            {
                popUpIndex++;
            }
        }

        if (popUpIndex == 3)
        {
            if (false)/**flip camera**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 4)
        {
            if (false)/**get fruit**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 5)
        {
            if (false)/**throw light ball**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 6)
        {
            if (false)/**shake off the witch**/ {
                popUpIndex++;
            }
        }

    }
}
