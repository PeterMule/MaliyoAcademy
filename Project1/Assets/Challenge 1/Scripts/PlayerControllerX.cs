using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private float speed=10f;
    private float rotationSpeed=50.0f;
    private float verticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.position = new Vector3(0.0f, transform.position.y, transform.position.z);

        // tilt the plane up/down based on up/down arrow keys
        transform.Rotate(new Vector3(rotationSpeed * verticalInput *  Time.deltaTime,0,0));
    }
}
