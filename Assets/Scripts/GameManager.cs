using UnityEngine;
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

    /***end fruit count stuff***/

    /*** gui***/
    private GUIStyle style = new GUIStyle();

    public Transform player1;
    public Transform player2;
    public Transform targetPlayer;
    /**public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }**/

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

        SceneManager.LoadScene("PlayerControls");
        numFruits1 = 0;
        numFruits2 = 0;
        gameWon = false;
        gameLost = false;
        //gameHasEnded = false;
    }

    public void setTargetPlayer(Transform target)
    {
        this.targetPlayer = target;
    }

    public Transform getTargetPlayer()
    {
        return this.targetPlayer;
    }

    /***fruit increment***/
    public void UpdateFruitCount(int playerNum)
    {
        if (playerNum == 1)
        {
            numFruits1 += 1;
        }

        if (playerNum == 2)
        {
            numFruits2 += 1;
        }
    }

    /***end of fruit increment***/

    /*** GUI display ***/
    private void OnGUI()
    {
        style.fontSize = 100;
        if (gameWon)
        {
            GUI.Label(new Rect(100, 100, 100, 200), "You Win!", style);
        }
        else if (gameLost)
        {
            GUI.Label(new Rect(100, 100, 100, 200), "You Lose!", style);
        }

        style.fontSize = 20;
        GUI.Label(new Rect(0, 0, 50, 50), "Fruit Count: " + numFruits1, style);
        GUI.Label(new Rect(70, 0, 50, 50), "Fruit Count: " + numFruits2, style);
    }

    /***end of GUI display***/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            /**SceneManager.LoadScene("PlayerControls");
            numFruits1 = 0;
            numFruits2 = 0;
            gameWon = false;
            gameLost = false;**/ //in restart function
            Restart();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        if (numFruits1 == 25 || numFruits2 == 25)
        {
            gameWon = true;
            //EndGame();

            Debug.Log("GAME WON");
            Invoke("Restart", restartDelay);
        }
    }
}
