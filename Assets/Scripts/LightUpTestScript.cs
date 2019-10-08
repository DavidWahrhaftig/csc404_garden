using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpTestScript : MonoBehaviour
{
    public GameObject enemyProjectile;
    private bool lightUp;
    private float lightTime = 10;
    private Color ogColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lightUp)
        {
            lightTime -= Time.smoothDeltaTime;

            if (lightTime < 0)
            {
                //Debug.Log("OG COLOR CHANGE!!!");
                lightUp = false;
                var playerRend = gameObject.GetComponent<Renderer>();
                playerRend.material.SetColor("_Color", ogColor);
                lightTime = 10;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == enemyProjectile.name + "(Clone)")
        {
            //Debug.Log("COLOR CHANGE!!!");
            var playerRend = gameObject.GetComponent<Renderer>();
            ogColor = playerRend.material.GetColor("_Color");
            playerRend.material.SetColor("_Color", Color.yellow);
            lightUp = true;
        }

        //else
        //{
        //    Debug.Log("Colliding Object: " + collision.gameObject.name);
        //    Debug.Log("Want Object: " + enemyProjectile.name);
        //}
    }
}
