using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBeyondScreen : MonoBehaviour
{

    private int leftLimit = -15;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if Obstacle is beyonf screen limits
        if(gameObject.CompareTag("Obstacle") && transform.position.x < leftLimit)
        {
            Destroy(gameObject);
            playerControllerScript.AddScore(1);
        }
    }
}
