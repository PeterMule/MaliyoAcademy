using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10.0f;
    private float zBounds = 10.5f;

    private Rigidbody playerRB;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ZConstraintPlayer();
    }

    //Move Player using Arrow Keys
    void MovePlayer()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        playerRB.AddForce(Vector3.forward * speed * vInput);
        playerRB.AddForce(Vector3.right * speed * hInput);
    }

    //Constrain Player allong the Z-Axis
    void ZConstraintPlayer()
    {

        if (transform.position.z > zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBounds);
        }
        else if (transform.position.z < -zBounds)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBounds);
        }
    }
}
