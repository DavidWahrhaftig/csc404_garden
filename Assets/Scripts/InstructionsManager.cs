using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] Rewired.Player gamePadController;

    [SerializeField] Animator fadeOut;

    
    // Start is called before the first frame update
    void Start()
    {
        gamePadController = Rewired.ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePadController.GetButtonDown("Jump"))
        {
            fadeOut.SetTrigger("FadeOut");
            Invoke("goToGameScene", 2.3f);
        }
            
        if (gamePadController.GetButtonDown("Camera Flip"))
        {
            fadeOut.SetTrigger("FadeOut");
            Invoke("goToMenuScene", 2.3f);
        }
            
    }

    void goToGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void goToMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
