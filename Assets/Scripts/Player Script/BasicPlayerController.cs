using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicPlayerController : MonoBehaviour
{
    //public float walkingSpeed;
    public float rotationSpeed = 50;
    public float movingSpeed = 20;

    public int jumpForce = 1;
    private Rigidbody selfRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
        //movingSpeed = walkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        respondToInput();

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("PlayerControls");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    private void respondToInput()
    {
        transform.Translate(0f, 0f, movingSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0, rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.J))
        {
            selfRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }
}

