using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FruitGainLoseUI : MonoBehaviour
{

    [Header("Fruit Gain & Lost Settings")]
    public Color losingFruit;
    public Color gainFruit;

    public TextMeshProUGUI fruitCounter1;
    public Color counter1Color;
    public TextMeshProUGUI fruitCounter2;
    public Color counter2Color;

    public float gainTransitionTime;

    public PlayerLogic playerLogic1;
    public PlayerLogic playerLogic2;

    private float time1, time2;

    // Start is called before the first frame update
    void Start()
    {
        playerLogic1 = GetComponent<GameManager>().player1.GetComponent<PlayerLogic>();
        playerLogic2 = GetComponent<GameManager>().player2.GetComponent<PlayerLogic>();
        time1 = gainTransitionTime;
        time2 = gainTransitionTime;
    }

    // Update is called once per frame
    void Update()
    {
        gainFruitTransition();

        //fruitCounter1.CrossFadeColor(); //Color.Lerp(gainFruit, counter1Color, time1 / gainTransitionTime);
        //fruitCounter2.color = Color.Lerp(gainFruit, counter2Color, time2 / gainTransitionTime);

        if (time1 < gainTransitionTime)
        {
            time1 += Time.deltaTime;
        }

        if (time2 < gainTransitionTime)
        {
            time2 += Time.deltaTime;
        }
    }


    public void gainFruitTransition()
    {
        if (playerLogic1.hasGainedFruit())
        {
            fruitCounter1.GetComponent<Animator>().SetTrigger("fruitGain");
            //time1 = 0f;
            playerLogic1.setGainedFruit(false);
            
        }

        if (playerLogic2.hasGainedFruit())
        {
            //time2 = 0f;
            fruitCounter2.GetComponent<Animator>().SetTrigger("fruitGain");
            playerLogic2.setGainedFruit(false);

        }
    }

}
