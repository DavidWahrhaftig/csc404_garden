using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    [SerializeField] Rewired.Player gamePadController;

    public Texture2D controlsImage;

    // Start is called before the first frame update
    void Start()
    {
        gamePadController = Rewired.ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePadController.GetButtonDown("Jump"))
            SceneManager.LoadScene("Milestone 5");
        if (gamePadController.GetButtonDown("Camera Flip"))
            SceneManager.LoadScene("MainMenu2");
    }
}
