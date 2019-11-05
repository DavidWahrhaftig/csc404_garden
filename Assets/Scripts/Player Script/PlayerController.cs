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
    public bool grounded = true; // flag

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
    float moveHorizontal;
    float rotateHorizontal;
    bool jumpButton;
    bool runButton;
    bool isCameraFlipped;
    int flip = 1;
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
        moveHorizontal = gamePadController.GetAxis("Move Horizontal");
        rotateHorizontal = gamePadController.GetAxis("Rotate");
        jumpButton = gamePadController.GetButtonDown("Jump");
        runButton = gamePadController.GetButton("Run");
        isCameraFlipped = gamePadController.GetButtonDown("Camera Flip");
        //bool centerToBase = gamePadController.GetButtonDown("CenterToBase");

        if (isCameraFlipped)
        {
            flip = flip * -1;
        }

        if (!playerLogic.isDisabled())
        {
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


            #region Running mechanics and Run Animation Transition
            if (runButton)//if (runButton && stamina > 0) // running
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
            #endregion

            if (jumpButton)
            {
                if (grounded)
                {
                    Jump(jumpForce, forceType);
                }
            }

            // forward/backward movement
            body.MovePosition(transform.position + transform.TransformDirection(moveHorizontal * flip, 0f, moveVertical) * movingSpeed * Time.deltaTime);
            // horizontal rotation movement
            transform.Rotate(0, rotationSpeed * rotateHorizontal * Time.fixedDeltaTime, 0);

            /*
            if (centerToBase) // Ceneter to base
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
        setGrounded(false);
        playSound(jumpSound);
        body.AddForce(transform.up * force, type);
        animator.SetBool("isJumping", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    public void levitate()
    {
        setGravity(false);
        setGrounded(false);
        body.AddForce(transform.up * 3, ForceMode.Force);
        
    }
    
    public void setGrounded(bool b)
    {
        grounded = b;
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


    public void setGravity(bool b)
    {
        body.useGravity = b;
    }

    public void won()
    {
        playerLogic.disableControls();
        animator.SetBool("isWinner", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);

    }

    public void lose()
    {
        playerLogic.disableControls();
        animator.SetBool("isLoser", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isJumping", false);

    }

 

}

