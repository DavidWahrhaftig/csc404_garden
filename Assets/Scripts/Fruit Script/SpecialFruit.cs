using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialFruit : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject neutralFruit;
    [SerializeField] GameObject redFruit;
    [SerializeField] GameObject blueFruit;

    [SerializeField] AudioClip[] fruitSounds;

    [SerializeField] float spellDuration = 5f; // how long a fruit will be under a spell before becoming neutral again
    [SerializeField] float respawnTime = 10f;

    GameObject currentActiveFruit;
    private AudioSource audioSource;
    private bool isUnderSpell = false;
    private bool isCollectable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        redFruit.SetActive(false);
        blueFruit.SetActive(false);

        currentActiveFruit = neutralFruit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCollectable)
        {
            if (other.gameObject.tag == "Player1"  || other.gameObject.tag == "Player2")
            {
                if (canPlyerTakeFruit(other.gameObject.tag))
                {
                    isCollectable = false;

                    other.gameObject.GetComponent<PlayerLogic>().incrementFruitCounter();
                    playFruitSound();
                    currentActiveFruit.SetActive(false);

                    Invoke("respawnFruit", respawnTime);
                }

            }
            else if (other.gameObject.tag == "ProjectileRed" || other.gameObject.tag == "ProjectileBlue")
            {
                putSpellOnFruit(other.gameObject.tag);
                other.gameObject.GetComponent<PropelLightOrb>().contactFruit();
            }
        }
    }

    private bool canPlyerTakeFruit(string playerTag)
    {
        GameObject enemyFruit = null;
        if (playerTag == "Player1")
        {
            enemyFruit = blueFruit;

        } else if (playerTag == "Player2")
        {
            enemyFruit = redFruit;
        }

        return currentActiveFruit != enemyFruit;
    }

    private void respawnFruit()
    {
        currentActiveFruit = neutralFruit;
        currentActiveFruit.SetActive(true);
        isCollectable = true;
    }

    private void playFruitSound()
    {
        if (fruitSounds.Length != 0)
        {
            int randomIndex = Random.Range(0, fruitSounds.Length);
            audioSource.PlayOneShot(fruitSounds[randomIndex]);
        }
    }

    public void putSpellOnFruit(string projectileTag)
    {
        if (projectileTag == "ProjectileRed")
        {
            if (currentActiveFruit == neutralFruit)
            {
                currentActiveFruit.SetActive(false);
                currentActiveFruit = redFruit;
            }
        }

        else if (projectileTag == "ProjectileBlue")
        {
            if (currentActiveFruit == neutralFruit)
            {
                currentActiveFruit.SetActive(false);
                currentActiveFruit = blueFruit;
            }
        }

        if (!isUnderSpell)
        {
            isUnderSpell = true;
            currentActiveFruit.SetActive(true);
            Invoke("spellWearOff", spellDuration);
        }
        
    }

    private void spellWearOff()
    {
        currentActiveFruit.SetActive(false);
        currentActiveFruit = neutralFruit;
        currentActiveFruit.SetActive(true);

        isUnderSpell = false;
    }

}
