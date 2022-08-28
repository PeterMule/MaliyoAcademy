using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoodForward : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 70f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move the food forward
        transform.Translate(0,0, Time.deltaTime * speed);

        //Rotate the food about its axis
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed * 30),Space.World);
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }
}
