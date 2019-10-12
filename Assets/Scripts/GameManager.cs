using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float restartDelay = 1f;


    /***Texts ***/
    public TextMeshProUGUI fruitCounter1;
    public TextMeshProUGUI fruitCounter2;
    public TextMeshProUGUI shotTimer1;
    public TextMeshProUGUI shotTimer2;
    public TextMeshProUGUI GameTimer;
    public TextMeshProUGUI gameResult1;
    public TextMeshProUGUI gameResult2;

    /*** Audio ***/
    [SerializeField] AudioClip[] fruitSounds;
    public AudioClip witchLaughSound, witchChasingSound, witchPatrollingSound, witchComplaningSound, witchIdleSound, gateOpenSound;

    public float gameDuration = 60;
    private float startTime;
    private AudioSource audioSource;


    public Transform player1;
    public Transform player2;
    public Transform targetPlayer;
    public Transform witch;
    public Transform witchBase;

    private float remainingTime;


    private void Start()
    {
        remainingTime = gameDuration;
        audioSource = GetComponent<AudioSource>();

        // set initial game UI
        fruitCounter1.text = "Fruit Count: 0";
        fruitCounter2.text = "Fruit Count: 0";
        shotTimer1.text = "Charge Full";
        shotTimer2.text = "Charge Full";
        gameResult1.text = "";
        gameResult2.text = "";

        startTime = Time.time;


        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]);
        }
    }

    void Update()
    {
        // Force Restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        // Quit Game
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        /*** UI Updates ***/
        updateLightBallChargeUI();

        fruitCounter1.text = "Fruit Count: " + player1.GetComponent<PlayerController>().getFruitCounter();
        fruitCounter2.text = "Fruit Count: " + player2.GetComponent<PlayerController>().getFruitCounter();

        updateTimer();

    }

    private void updateTimer()
    {
        remainingTime = gameDuration - (Time.time - startTime);

        string minutes = ((int)remainingTime / 60).ToString();
        string seconds = (remainingTime % 60).ToString("f0");


        // winner detection
        if (remainingTime <= Mathf.Epsilon)
        {
            if (player1.GetComponent<PlayerController>().getFruitCounter() > player2.GetComponent<PlayerController>().getFruitCounter())
            {
                gameResult1.text = "Merlin's Apprentice";
                gameResult2.text = "Unemployed";
            }
            else if (player1.GetComponent<PlayerController>().getFruitCounter() < player2.GetComponent<PlayerController>().getFruitCounter())
            {
                gameResult2.text = "Merlin's Apprentice";
                gameResult1.text = "Unemployed";
            }
            else
            {
                gameResult1.text = "No One Likes Ties\nPlay Again";
                gameResult2.text = "No One Likes Ties\nPlay Again";
            }

            player1.GetComponent<PlayerController>().disableControls();
            player2.GetComponent<PlayerController>().disableControls();

            // restart game when pressing A button
            if (Input.GetButton("Jump" + player1.GetComponent<PlayerController>().gamePad) || Input.GetButton("Jump" + player2.GetComponent<PlayerController>().gamePad)) 
            {
                Restart();
            }
        }
        else
        {
            GameTimer.text = minutes + ":" + seconds;
        }
    }

    private void updateLightBallChargeUI()
    {
        if (player1.GetComponent<SpawnLightOrb>().getSpawnTimer() != player1.GetComponent<SpawnLightOrb>().getReloadTime())
        {
            shotTimer1.text = "Charging ... " + player1.GetComponent<SpawnLightOrb>().getSpawnTimer().ToString("f0");
        }
        else
        {
            shotTimer1.text = "Charge Full";
        }

        if (player2.GetComponent<SpawnLightOrb>().getSpawnTimer() != player2.GetComponent<SpawnLightOrb>().getReloadTime())
        {
            shotTimer2.text = "Charging ... " + player2.GetComponent<SpawnLightOrb>().getSpawnTimer().ToString("f0");
        }
        else
        {
            shotTimer2.text = "Charge Full";
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Milestone 2");
    }

    public Transform getTargetPlayer()
    {
        return this.targetPlayer;
    }

    public void setTargetPlayer(Transform target)
    {
        this.targetPlayer = target;
    }

    public Transform getWitch()
    {
        return this.witch;
    }

    public Transform getWitchBase()
    {
        return witchBase;
    }

    public void playFruitSound()
    {
        if (fruitSounds.Length == 0) { return; } // if no sounds were added
        int index = Random.Range(0, fruitSounds.Length);

        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(fruitSounds[index]);
        }
    }

    public void playSound(AudioClip audio)
    {
        if (!audioSource.isPlaying) // so it doesn't layer
        {
            audioSource.PlayOneShot(audio);
        }
    }



}
