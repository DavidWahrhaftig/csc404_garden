using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int gamePad;
    public float walkingSpeed, rotationSpeed, stamina;
    public float centerToBaseSpeed;
    public int jumpForce;

    public AudioClip walkingSound, jumpSound;
    private AudioSource audioSource;

    private float movingSpeed;
    private float defaultY;

    private Rigidbody selfRigidbody;

    private GameManager gameManager;
    private PlayerLogic playerLogic;
    
    /** Flags **/
    public bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        defaultY = transform.position.y;
        movingSpeed = walkingSpeed;

        selfRigidbody = GetComponent<Rigidbody>();

        gameManager = FindObjectOfType<GameManager>();
        playerLogic = GetComponent<PlayerLogic>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerLogic.isDisabled)
        {
            respondToInput();
        }
    }

    private void respondToInput()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical" + gamePad) * Time.deltaTime);
        transform.Translate(movingSpeed * Input.GetAxis("Horizontal" + gamePad) * Time.deltaTime, 0f, 0f);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("HorizontalRot" + gamePad) * Time.deltaTime, 0);

        if (Input.GetButton("CenterToBase" + gamePad)) // Ceneter to base
        {
            Vector3 targetDir = playerLogic.playerBase.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, centerToBaseSpeed * Time.deltaTime, 0.0f);
            Vector3 newestDir = new Vector3(newDir.x, defaultY, newDir.z);
            transform.rotation = Quaternion.LookRotation(newestDir);
        }

        if (Input.GetButton("Jump" + gamePad))
        {
            Debug.Log("Jumping");
            if (canJump)
            {
                playSound(jumpSound);
                selfRigidbody.AddForce(Vector3.up * jumpForce);
                canJump = false;
            }
        }

        if (Input.GetButton("Run" + gamePad) && stamina > 0) // running
        {
            playSound(walkingSound);
            movingSpeed = walkingSpeed * 2;
            stamina -= 0.1f;
        }
        else
        {
            movingSpeed = walkingSpeed;
            stamina += 0.01f;
        }
    }

    public void disableControls()
    {
        playerLogic.isDisabled = true;
    }
    public void enableControls()
    {
        playerLogic.isDisabled = false;
        playerLogic.stopChasingMe();
    }

    public void setCanJump(bool b)
    {
        canJump = b;
    }

    public void playSound( AudioClip audio)
    {
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(audio);
        }
    }
}

