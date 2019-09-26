using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movingSpeed = 20;
    public float rotationSpeed = 100;

    int numFruits = 0;

    bool gameWon = false;
    bool gameLost = false;
    [SerializeField] AudioClip witchSound;
    [SerializeField] AudioClip[] fruitSounds;
    FruitBushScript fruitBushScript;

    AudioSource audioSource;

    private GUIStyle style = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        respondToInput();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            numFruits = 0;
            gameWon = false;
            gameLost = false;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        if (numFruits == 25)
        {
            gameWon = true;
        }

        //Debug.Log(numFruits);
    }

    private void respondToInput()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "FruitBush")
        {
            GameObject fruitBush = GameObject.Find(collision.transform.name);
            fruitBushScript = fruitBush.GetComponent<FruitBushScript>();
            playFruitSound();
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            numFruits += fruitBushScript.fruitsInBush;
            fruitBushScript.fruitsInBush = 0;
        }

        if (collision.transform.tag == "Witch")
        {
            audioSource.Stop();
            audioSource.PlayOneShot(witchSound);
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            gameLost = true;
        }
    }

    private void playFruitSound()
    {
        if (fruitSounds.Length == 0) { return; } // if no sounds were added
        int index = Random.Range(0, fruitSounds.Length);

        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(fruitSounds[index]);
        }
    }

    private void OnGUI()
    {
        style.fontSize = 100;
        if (gameWon)
        {
            GUI.Label(new Rect(100, 100, 100, 200), "You Win!", style);
        }
        else if (gameLost)
        {
            GUI.Label(new Rect(100, 100, 100, 200), "You Lose!", style);
        }
    }
}

