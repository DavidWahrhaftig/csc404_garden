using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public Transform playerBase;

    public GameObject enemyProjectile;

    public AudioClip hitSound, caughtSound;


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
    public bool canChase = true;

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
        }
    }


    public void changeColor(Color color)
    {
        //var playerRend = gameObject.GetComponent<Renderer>();
        playerSkin.material.SetColor("_Color", color);
    }

    public void chaseMe()
    {
        WitchLogic witchLogic = gameManager.getWitch().GetComponent<WitchLogic>();
        witchLogic.chase(transform);

        changeColor(Color.white);
        setGlowing(true); // disable this player from shooting in SpawnLightOrb.cs 

    }

    public void stopChasingMe()
    {
        WitchLogic witchLogic = gameManager.getWitch().GetComponent<WitchLogic>();
        witchLogic.stopChasing();

        changeColor(ogColor);
        setGlowing(false);
        setHidden(false);        
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
        disabled = true;
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

    public void setCaught(bool b)
    {
        this.caught = b;
    }

    public bool isDisabled()
    {
        return this.disabled;
    }

    public bool getCanChase()
    {
        return this.canChase;
    }

    public void setCanChase(bool b)
    {
        this.canChase = b;
    } 
}
