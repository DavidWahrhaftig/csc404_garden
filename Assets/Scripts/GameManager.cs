using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /***game win, lose, restart***/
    bool gameWon = false;
    bool gameLost = false;

    //bool gameHasEnded = false;
    public float restartDelay = 1f;

    /***fruit count stuff ***/
    public int numFruits1 = 0;
    public int numFruits2 = 0;

    /***Texts ***/
    public TextMeshProUGUI fruitCounter1;
    public TextMeshProUGUI fruitCounter2;
    public TextMeshProUGUI shotTimer1;
    public TextMeshProUGUI shotTimer2;
    public TextMeshProUGUI GameTimer;
    public TextMeshProUGUI gameResult1;
    public TextMeshProUGUI gameResult2;

    public float gameDuration = 60;
    private float startTime;
    /***end fruit count stuff***/

    /*** gui***/
    //private GUIStyle style = new GUIStyle();

    public Transform player1;
    public Transform player2;
    public Transform targetPlayer;
    public Transform witch;
    /**public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }**/

    private void Start()
    {
        fruitCounter1.text = "Fruit Count: 0";
        fruitCounter2.text = "Fruit Count: 0";
        shotTimer1.text = "Charge Full";
        shotTimer2.text = "Charge Full";
        gameResult1.text = "";
        gameResult2.text = "";
        startTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }


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

        fruitCounter1.text = "Fruit Count: " + player1.GetComponent<PlayerController>().getFruitCounter();
        fruitCounter2.text = "Fruit Count: " + player2.GetComponent<PlayerController>().getFruitCounter();

        float t = gameDuration - (Time.time - startTime);
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f0");
        
     
        // winner detection
        if (t <= Mathf.Epsilon)
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

            if (Input.GetButton("Jump")) // restart game when pressing A button
            {
                Restart();
            }
        }
        else
        {
            GameTimer.text = minutes + ":" + seconds;
        }

    }

    public void GameLost()
    {
        if (gameLost == false)
        {
            gameLost = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene("Milestone 2");
    }

    public void setTargetPlayer(Transform target)
    {
        this.targetPlayer = target;
    }

    public Transform getTargetPlayer()
    {
        return this.targetPlayer;
    }


    /*** GUI display ***/
    private void OnGUI()
    {
        //style.fontSize = 100;
        if (gameWon)
        {
            //GUI.Label(new Rect(100, 100, 100, 200), "You Win!", style);
        }
        else if (gameLost)
        {
            //GUI.Label(new Rect(100, 100, 100, 200), "You Lose!", style);
        }

        //style.fontSize = 20;
        //GUI.Label(new Rect(0, 0, 50, 50), "Fruit Count: " + numFruits1, style);
        //GUI.Label(new Rect(70, 0, 50, 50), "Fruit Count: " + numFruits2, style);
    }

    /***end of GUI display***/

    
    public Transform getWitch()
    {
        return this.witch;
    }

    
}
