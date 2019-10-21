using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
public class PlayerController : MonoBehaviour
{
    public int gamePadID;
    [SerializeField] Rewired.Player gamePadController; 

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
        gamePadController = Rewired.ReInput.players.GetPlayer(gamePadID);
        Debug.Log(gamePadID); // not printing in console?
        gameManager = FindObjectOfType<GameManager>();

        playerLogic = GetComponent<PlayerLogic>();
        selfRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        defaultY = transform.position.y;
        movingSpeed = walkingSpeed;

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
        // getting input from game pad controller
        float moveVertical = gamePadController.GetAxis("Move Vertical");
        float rotateHorizontal = gamePadController.GetAxis("Rotate");
        float lookVertical = gamePadController.GetAxis("Look Vertical"); // in LookVertically.cs
        bool jump = gamePadController.GetButtonDown("Jump");
        bool run = gamePadController.GetButton("Run");
        bool shoot = gamePadController.GetButtonDown("Shoot");
        //bool centerToBase = gamePadController.GetButtonDown("CenterToBase");
        


        transform.Translate(0f, 0f, movingSpeed * moveVertical * Time.deltaTime);
        //transform.Translate(movingSpeed * Input.GetAxis("Horizontal" + gamePadID) * Time.deltaTime, 0f, 0f);
        transform.Rotate(0, rotationSpeed * rotateHorizontal * Time.deltaTime, 0);

        /*
        if (Input.GetButton("CenterToBase" + gamePadID)) // Ceneter to base
        {
            Vector3 targetDir = playerLogic.playerBase.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, centerToBaseSpeed * Time.deltaTime, 0.0f);
            Vector3 newestDir = new Vector3(newDir.x, defaultY, newDir.z);
            transform.rotation = Quaternion.LookRotation(newestDir);
        }*/

        if (jump)
        {
            Debug.Log("Jumping");
            if (canJump)
            {
                playSound(jumpSound);
                selfRigidbody.AddForce(Vector3.up * jumpForce);
                canJump = false;
            }
        }

        if (run && stamina > 0) // running
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

    public Rewired.Player getGamePadController()
    {
        return this.gamePadController;
    }
}

