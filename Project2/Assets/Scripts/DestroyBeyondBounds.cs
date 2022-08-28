using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBeyondBounds : MonoBehaviour
{
    // Start is called before the first frame update
    private float xRangeL = -25.0f;
    private float xRangeR = 25.0f;

    private float hheight = 34.0f;
    private float lheight = -16.0f;
    private GameManager gameManager;



    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


    }
    // Update is called once per frame
    void Update()
    {
        // Destroy if object beyond x limits
        if (transform.position.x < xRangeL || transform.position.x > xRangeR)
        {
            if(gameObject.CompareTag("Animal"))
            {
                gameManager.Addlives(-1);
            }
            
            Destroy(gameObject);
        }
        // Destroy if object beyond  z limits
        else if (transform.position.z < lheight || transform.position.z > hheight)
        {
            if (gameObject.CompareTag("Animal"))
            {
                gameManager.Addlives(-1);
            }
            Destroy(gameObject);
        }

    }
}
