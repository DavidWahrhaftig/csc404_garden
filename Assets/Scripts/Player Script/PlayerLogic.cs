using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public Transform playerBase;

    public GameObject enemyProjectile;
    public float lightTime = 1000;

    public AudioClip hitSound;

    [SerializeField] SkinnedMeshRenderer playerSkin;

    private AudioSource audioSource;

    private Rigidbody selfRigidbody;
    private GameManager gameManager;
    public int fruitCounter = 0;

    private Color ogColor;

    //Flags
    private bool isGlowing = false; // for witch and hidden ability
    public bool isDisabled = false;
    public bool isHidden = false;
    public bool isCaught = false;

    private float yRotation;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        selfRigidbody = GetComponent<Rigidbody>();

        //var playerRend = gameObject.GetComponent<Renderer>();
        ogColor = playerSkin.material.GetColor("_Color");

        gameManager = FindObjectOfType<GameManager>();
        playerController = GetComponent<PlayerController>();

        yRotation = transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isGlowing)
        {
            lightTime -= Time.smoothDeltaTime;

            if (lightTime < 0) // keep player lit up until timer runs out 
            {

                isGlowing = false;
                lightTime = 10;
                if (!isCaught) { stopChasingMe(); }

            }
            if (isHidden)
            {
                // change color back to ogColor and set isGlowing to false
                // Change witch state to patrol
                stopChasingMe();
            }
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
        }

        if (collision.transform.tag == "Ground")
        {
            playerController.isInAir = false;
            playerController.getAnimator().SetBool("isJumping", false);
        }

        if (collision.gameObject.name == enemyProjectile.name + "(Clone)") // TODO: change this, don't use Object name, maybe use Tag
        {
            chaseMe();
            playSound(hitSound);

            // disable this player from shooting?
        }
    }


    public void changeColor(Color color)
    {
        //var playerRend = gameObject.GetComponent<Renderer>();
        playerSkin.material.SetColor("_Color", color);
    }

    public void chaseMe()
    {
        changeColor(Color.white);
        isGlowing = true;

        // change witch state to 'Chase'
        Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
        gameManager.setTargetPlayer(transform);
        witchAnimator.SetBool("isChasing", true);
        witchAnimator.SetBool("isIdle", false);
        witchAnimator.SetBool("isPatrolling", false);
        
    }
    public void stopChasingMe()
    {
        changeColor(ogColor);
        isGlowing = false;
        // change witch state to 'Patrol'
        Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
        witchAnimator.SetBool("isChasing", false);
        witchAnimator.SetBool("isIdle", false);
        witchAnimator.SetBool("isPatrolling", true);
        gameManager.setTargetPlayer(null);
        isHidden = false;
    }

    public void spawn()
    {
        transform.position = playerBase.position;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        playerController.getAnimator().SetBool("isGettingUp", true);
        //playerController.getAnimator().SetBool("isCaught", false);
    }

    public bool getIsHidden()
    {
        return isHidden;
    }
    public void setIsHidden(bool b)
    {
        isHidden = b;
    }

    public void setIsGlowing(bool b)
    {
        isGlowing = b;
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

    public void playSound(AudioClip audio)
    {
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(audio);
        }
    }


    public void gotCaught()
    {
        isCaught = true;
    }

    public void disableControls()
    {
        isDisabled = true;
    }
    public void enableControls()
    {
        isCaught = false;
        isDisabled = false;
        //stopChasingMe(); // moved to PlayerRespawnBehaviour.cs
    }

    public bool getIsCaught()
    {
        return this.isCaught;
    }

    public bool getIsDisabled()
    {
        return this.isDisabled;
    }
}
