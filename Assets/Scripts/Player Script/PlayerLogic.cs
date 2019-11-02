using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public Transform playerBase;

    public GameObject enemyProjectile;

    public AudioClip hitSound;


    [SerializeField] SkinnedMeshRenderer playerSkin;

    private AudioSource audioSource;

    private Rigidbody selfRigidbody;
    private GameManager gameManager;
    public int fruitCounter = 0;

    private Color ogColor;

    //Flags
    public bool glowing = false; // for witch and hidden ability
    public bool disabled = false;
    public bool hidden = false;
    public bool caught = false;

    private float yRotation;
    private PlayerController playerController;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        selfRigidbody = GetComponent<Rigidbody>();

        ogColor = playerSkin.material.GetColor("_Color");

        gameManager = FindObjectOfType<GameManager>();
        playerController = GetComponent<PlayerController>();

        yRotation = transform.rotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (glowing)
        {
            if (hidden) { stopChasingMe(); }
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

        if (collision.transform.tag == "Ground")
        {
            playerController.setGrounded(true);
            playerController.getAnimator().SetBool("isJumping", false);
        }

        if (collision.gameObject.name == enemyProjectile.name + "(Clone)") // TODO: change this, don't use Object name, maybe use Tag
        {
            playSound(hitSound);
            chaseMe();
            
            // disable this player from shooting in SpawnLightOrb.cs 
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
        glowing = true;

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
        setGlowing(false);
        setHidden(false);

        // change witch state to 'Patrol'
        Animator witchAnimator = gameManager.getWitch().GetComponent<Animator>();
        witchAnimator.SetBool("isChasing", false);
        witchAnimator.SetBool("isIdle", false);
        witchAnimator.SetBool("isPatrolling", true);
        gameManager.setTargetPlayer(null);
        
    }

    public void spawn()
    {
        transform.position = playerBase.position;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        playerController.getAnimator().SetBool("isGettingUp", true);
        //playerController.getAnimator().SetBool("isCaught", false);
    }

    public bool isHidden()
    {
        return hidden;
    }

    public void setHidden(bool b)
    {
        hidden = b;
    }

    public bool isGlowing()
    {
        return glowing;
    }

    public void setGlowing(bool b)
    {
        glowing = b;
    }


    public int getFruitCounter()
    {
        return fruitCounter;
    }

    public void incrementFruitCounter()
    {
        fruitCounter += 1;
        //gameManager.playFruitSound();
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
        caught = true;
        animator.SetBool("isIdle", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);

    }

    public void disableControls()
    {
        disabled = true;
    }

    public void enableControls()
    {
        caught = false;
        disabled = false;
        //stopChasingMe(); // moved to PlayerRespawnBehaviour.cs
    }

    public bool isCaught()
    {
        return this.caught;
    }

    public bool isDisabled()
    {
        return this.disabled;
    }
}
