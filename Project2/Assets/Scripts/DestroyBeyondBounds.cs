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

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        // Destroy dogs if x position less than left limit
        if (transform.position.x < xRangeL || transform.position.x > xRangeR)
        {
            Destroy(gameObject);
        }
        // Destroy balls if y position is less than bottomLimit
        else if (transform.position.z < lheight || transform.position.z > hheight)
        {
            Destroy(gameObject);
        }

    }
}
