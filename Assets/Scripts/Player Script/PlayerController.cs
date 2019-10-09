using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkingSpeed, rotationSpeed, stamina;
    public Transform playerBase;
    public float centerToBaseSpeed;
    public GameObject enemyProjectile;
    public int jumpForce;

    public int gamePad;

    public AudioClip walkingSound, jumpSound, hitSound;

    private AudioSource audioSource;


    private float movingSpeed;
    private float defaultY;
    private float lightTime = 10;
    private Color ogColor;
    private Rigidbody selfRigidbody;
    private GameManager gameManager;
    private int fruitCounter = 0;
    
    /** Flags **/
    private bool canJump = true;
    private bool isGlowing = false; // for witch and hidden ability
    private bool isDisabled = false;
    public bool isHidden = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selfRigidbody = GetComponent<Rigidbody>();
        defaultY = transform.position.y;
        movingSpeed = walkingSpeed;
        var playerRend = gameObject.GetComponent<Renderer>();
        ogColor = playerRend.material.GetColor("_Color");

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDisabled)
        {
            selfRigidbody.useGravity = false;
            if (canJump)
            {
                
                Invoke("liftInAir", 1);
                canJump = false;
            }
        }
        else {
            selfRigidbody.useGravity = true;
            respondToInput();}

        if (isGlowing)
        {
            lightTime -= Time.smoothDeltaTime;

            if (lightTime < 0) // keep player lit up until timer runs out 
            {

                isGlowing = false;
                lightTime = 10;
                if (!isDisabled) { stopChasingMe(); }
                
            }
            if (isHidden)
            {
                // change color back to ogColor and set isGlowing to false
                // change witch state to chase
                stopChasingMe();
            }
        }
    }

    private void liftInAir()
    {
        selfRigidbody.AddForce(Vector3.up * 200);
    }
    public void changeColor(Color color)
    {
        var playerRend = gameObject.GetComponent<Renderer>();
        playerRend.material.SetColor("_Color", color);
    }

    private void respondToInput()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical" + gamePad) * Time.deltaTime);
        transform.Translate(movingSpeed * Input.GetAxis("Horizontal" + gamePad) * Time.deltaTime, 0f, 0f);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("HorizontalRot" + gamePad) * Time.deltaTime, 0);

        if (Input.GetButton("CenterToBase" + gamePad)) // Ceneter to base
        {
            Vector3 targetDir = playerBase.position - transform.position;
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

  
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Fruit")
        {
            //playFruitSound();
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            
            Debug.Log("FruitCounter: " + fruitCounter);

        }

        if (collision.transform.tag == "Witch")
        {
            audioSource.Stop();
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            transform.position = playerBase.transform.position;
            // gameLost = true;
            FindObjectOfType<GameManager>().GameLost(); //added
        }

        if (collision.transform.tag == "Ground")
            canJump = true;

        if (collision.gameObject.name == enemyProjectile.name + "(Clone)")
        {
            chaseMe();

            playSound(hitSound);

            // disable this player from shooting

        }
    }

    public void chaseMe()
    {
        changeColor(Color.white);
        isGlowing = true;

        // change witch state to chase
        Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
        witchAnimator.SetBool("isChasing", true);
        witchAnimator.SetBool("isIdle", false);
        witchAnimator.SetBool("isPatrolling", false);
        gameManager.setTargetPlayer(transform);
    }
    public void stopChasingMe()
    {
        changeColor(ogColor);
        isGlowing = false;
        // change witch state to patrol
        Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
        witchAnimator.SetBool("isChasing", false);
        witchAnimator.SetBool("isIdle", false);
        witchAnimator.SetBool("isPatrolling", true);
        gameManager.setTargetPlayer(null);
        isHidden = false;
    }

    public void disableControls()
    {
        isDisabled = true;
        
    }

    public void enableControls()
    {
        isDisabled = false;
        stopChasingMe();
    }

    public void spawn()
    {
        transform.position = playerBase.position;
    }

    public bool getIsHidden()
    {
        return isHidden;
    }

    public void setIsHidden(bool b)
    {
       isHidden = b;       
    }

    public void setCanJump(bool b)
    {
        canJump = b;
    }

    public void setIsGlowing(bool b)
    {
        isGlowing = true;
    }
    public bool getIsGlowing()
    {
        return isGlowing;
    }

    public int getFruitCounter()
    {
        return fruitCounter;
    }
    
    public void incrementFruitCounter()
    {
        fruitCounter += 1;
        gameManager.playFruitSound();
    }

    public void loseFruits()
    {
        fruitCounter = 0;
    }

    public void playSound( AudioClip audio)
    {
   
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(audio);
        }
    }
}

