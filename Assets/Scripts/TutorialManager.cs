using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    private void Update()
    {
        for (int - = 0; i < popUps.Length; i++) {
            if (if == popUpIndex)
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
            if /**left and right movement**/ {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 1)
        {
            if /**jump**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 2)
        {
            if /**rotate camera**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 3)
        {
            if /**flip camera**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 4)
        {
            if /**get fruit**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 5)
        {
            if /**throw light ball**/ {
                popUpIndex++;
            }
        }

        if (popUpIndex == 6)
        {
            if /**shake off the witch**/ {
                popUpIndex++;
            }
        }

    }
}
