using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 12.0f;
    private float width = 20.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        if(transform.position.x > width)
        {
            transform.position = new Vector3(width ,0,0);
        }
        else if(transform.position.x < (-1*width))
        {
            transform.position = new Vector3(width * -1, 0, 0);
        }

        transform.Translate(transform.right * hInput * Time.deltaTime * speed); 
    }
}
