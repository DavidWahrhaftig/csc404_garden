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
            SceneManager.LoadScene("MainMenu2");
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, -10, Screen.width, Screen.height * 1.03f), controlsImage);
    }
}
