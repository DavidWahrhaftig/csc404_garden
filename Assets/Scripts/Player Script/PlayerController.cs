using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
public class PlayerController : MonoBehaviour
{
    [Header("Game Pad Controller Settings")]
    public int gamePadID;
    [SerializeField] Rewired.Player gamePadController;

    [Header("Speed Settings")]
    public float walkingSpeed;
    public float rotationSpeed;
    public float stamina;
    public float centerToBaseSpeed;

    [Header("Jump Settings")]
    public float jumpForce;
    public ForceMode forceType;
    public bool isInAir = false; // flag

    [Header("Sound Settings")]
    public AudioClip walkingSound;
    public AudioClip jumpSound;


    #region Private Fields
    private AudioSource audioSource;
    private float movingSpeed;
    private float defaultY;
    private Rigidbody body;
    private GameManager gameManager;
    private PlayerLogic playerLogic;
    private Animator animator;
    #endregion

    #region Controller Inputs
    float moveVertical;
    float rotateHorizontal;
    bool jumpButton;
    bool runButton;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        #region Components and GameObjects Referencing
        gamePadController = Rewired.ReInput.players.GetPlayer(gamePadID);
        gameManager = FindObjectOfType<GameManager>();
        playerLogic = GetComponent<PlayerLogic>();
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        #endregion 

        //defaultY = transform.position.y;
        movingSpeed = walkingSpeed;

    }

    /**
     * Update runs at the same frequency as the game's framerate. 
     * If you're getting 100 FPS, then Update() runs 100 times per second. 
    **/
    private void Update()
    {
        moveVertical = gamePadController.GetAxis("Move Vertical");
        rotateHorizontal = gamePadController.GetAxis("Rotate");
        jumpButton = gamePadController.GetButtonDown("Jump");
        runButton = gamePadController.GetButton("Run");
        //bool centerToBase = gamePadController.GetButtonDown("CenterToBase");

        #region Idle & Walk Animation Transitions
        if (Mathf.Abs(moveVertical) > Mathf.Epsilon)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
        }
        #endregion

        if (jumpButton)
        {
            if (!isInAir)
            {
                Jump(jumpForce, forceType);
            }
        }

        if (runButton && stamina > 0) // running
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", false);

            playSound(walkingSound);
            movingSpeed = walkingSpeed * 2;
            stamina -= 0.1f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            movingSpeed = walkingSpeed;
            stamina += 0.01f;
        }
    }

    /**
     * FixedUpdate is specifically designed for physics updates e.g. movements, raycasts, collisions, etc...
     * It is where physics calculations and changes should go since it runs at a constant 50 FPS to match the physics engine.
    **/
    void FixedUpdate()
    {
        if (!playerLogic.isDisabled)
        {
            //respondToInput();
            // forward/backward movement
            body.MovePosition(transform.position + transform.TransformDirection(0f, 0f, moveVertical) * movingSpeed * Time.deltaTime);
            // horizontal rotation movement
            transform.Rotate(0, rotationSpeed * rotateHorizontal * Time.fixedDeltaTime, 0);

            /*
            if (Input.GetButton("CenterToBase" + gamePadID)) // Ceneter to base
            {
                Vector3 targetDir = playerLogic.playerBase.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, centerToBaseSpeed * Time.deltaTime, 0.0f);
                Vector3 newestDir = new Vector3(newDir.x, defaultY, newDir.z);
                transform.rotation = Quaternion.LookRotation(newestDir);
            }*/
        }
    }


    void Jump(float force, ForceMode type)
    {
        isInAir = true;
        playSound(jumpSound);
        body.AddForce(transform.up * force, type);
        animator.SetBool("isJumping", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
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

    public void setIsJumping(bool b)
    {
        isInAir = b;
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

    public Animator getAnimator()
    {
        return this.animator;
    }
}

