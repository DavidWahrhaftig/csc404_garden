﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed, rotationSpeed, stamina;
    private float movingSpeed;

    public Transform playerBase;
    public float centerToBaseSpeed;
    private float defaultY;

    public int jumpForce;
    private bool canJump;
    private Rigidbody selfRigidbody;

    int numFruits = 0;

    bool gameWon = false;
    bool gameLost = false;
    [SerializeField] AudioClip witchSound;  // TODO: move to Witch script
    [SerializeField] AudioClip[] fruitSounds;
    FruitBushScript fruitBushScript;

    AudioSource audioSource;

    private GUIStyle style = new GUIStyle();

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selfRigidbody = GetComponent<Rigidbody>();
        defaultY = transform.position.y;
        movingSpeed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        respondToInput();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("PlayerControls");
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
    }

    private void respondToInput()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Translate(movingSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, 0f);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("HorizontalRot") * Time.deltaTime, 0);

        if (Input.GetButton("Fire1"))
        {
            Vector3 targetDir = playerBase.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, centerToBaseSpeed * Time.deltaTime, 0.0f);
            Vector3 newestDir = new Vector3(newDir.x, defaultY, newDir.z);
            transform.rotation = Quaternion.LookRotation(newestDir);
        }

        if (Input.GetButton("Jump"))
        {
            if (canJump)
            {
                selfRigidbody.AddForce(Vector3.up * jumpForce);
                canJump = false;
            }
        }

        if (Input.GetButton("Fire2") && stamina > 0)
        {
            movingSpeed = walkingSpeed * 2;
            stamina -= 0.1f;
        }
        else
        {
            movingSpeed = walkingSpeed;
            stamina += 0.01f;
        }
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
            transform.position = playerBase.transform.position;
            gameLost = true;
        }

        if (collision.transform.tag == "Ground")
            canJump = true;
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

        style.fontSize = 20;
        GUI.Label(new Rect(0, 0, 50, 50), "Fruit Count: " + numFruits, style);
    }
}
