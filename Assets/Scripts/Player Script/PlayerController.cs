using System.Collections;
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
    public GameObject enemyProjectile;
    private bool isLightUp;
    private float lightTime = 10;
    private Color ogColor;
    public int jumpForce;
    private bool canJump = true;
    private Rigidbody selfRigidbody;
    /**int numFruits = 0;
    bool gameWon = false;
    bool gameLost = false;**/
    [SerializeField] AudioClip[] fruitSounds;
    FruitBushScript fruitBushScript;
    AudioSource audioSource;
    GameManager gameManager;
    private int fruitCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selfRigidbody = GetComponent<Rigidbody>();
        defaultY = transform.position.y;
        movingSpeed = walkingSpeed;

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        respondToInput();

        if (isLightUp)
        {
            lightTime -= Time.smoothDeltaTime;

            if (lightTime < 0) // keep player lit up until timer runs out 
            {

                isLightUp = false;
                var playerRend = gameObject.GetComponent<Renderer>();
                playerRend.material.SetColor("_Color", ogColor);
                lightTime = 10;
            }
        }
    }

    private void respondToInput()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical1") * Time.deltaTime);
        transform.Translate(movingSpeed * Input.GetAxis("Horizontal1") * Time.deltaTime, 0f, 0f);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("HorizontalRot") * Time.deltaTime, 0);

        if (Input.GetButton("Fire1")) // Ceneter to base
        {
            Vector3 targetDir = playerBase.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, centerToBaseSpeed * Time.deltaTime, 0.0f);
            Vector3 newestDir = new Vector3(newDir.x, defaultY, newDir.z);
            transform.rotation = Quaternion.LookRotation(newestDir);
        }

        if (Input.GetButton("Jump"))
        {
            Debug.Log("Jumping");
            if (canJump)
            {
                selfRigidbody.AddForce(Vector3.up * jumpForce);
                canJump = false;
            }
        }

        if (Input.GetButton("Fire2") && stamina > 0) // running
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

    private void playFruitSound()
    {
        if (fruitSounds.Length == 0) { return; } // if no sounds were added
        int index = Random.Range(0, fruitSounds.Length);

        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(fruitSounds[index]);
        }
    }

    public void setCanJump(bool b)
    {
        canJump = b;
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.transform.tag == "Fruit")
        {
            //GameObject fruitBush = GameObject.Find(collision.transform.name);
            //fruitBushScript = fruitBush.GetComponent<FruitBushScript>();
            //numFruits += fruitBushScript.fruitsInBush;
            //FindObjectOfType<GameManager>().updateFruitCount(1); //added
            //fruitBushScript.fruitsInBush = 0;
            playFruitSound();
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
            //Debug.Log("COLOR CHANGE!!!");
            var playerRend = gameObject.GetComponent<Renderer>();
            ogColor = playerRend.material.GetColor("_Color");
            playerRend.material.SetColor("_Color", Color.white);
            isLightUp = true;

            // change witch state to chase
            Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
            witchAnimator.SetBool("isChasing", true);
            witchAnimator.SetBool("isIdle", false);
            witchAnimator.SetBool("isPatrolling", false);
            gameManager.setTargetPlayer(transform);

            // disable this player from shooting
        
        }
    }

    public int getFruitCounter()
    {
        return fruitCounter;
    }
    
    public void incrementFruitCounter()
    {
        fruitCounter += 1;
    }
}

