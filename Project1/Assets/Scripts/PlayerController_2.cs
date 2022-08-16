using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_2 : MonoBehaviour
{
    private float speed = 30.0f;
    private float turnSpeed = 40.0f;
    private float horizontalInput = 0.0f;
    private float forwardInput = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal_2");
        forwardInput = Input.GetAxis("Vertical_2");
        //Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //Rotate the car base on speed
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput * forwardInput);
        //transform.(0.0f, 20.0f * horizontalInput * turnSpeed, 0.0f);
    }
}
