using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 30.0f;
    private float width = 23.0f;
    private float hheight = 31.0f;
    private float lheight = -13.0f;
    public GameObject projectilePrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get Horizontal Input
        float hInput = Input.GetAxis("Horizontal");

        //Get Horizontal Input
        float vInput = Input.GetAxis("Vertical");

        //Move player left and right
        transform.Translate(transform.right * hInput * Time.deltaTime * speed);

        //Move player up and down
        transform.Translate(transform.forward * vInput * Time.deltaTime * speed);

        //Reset player position to stay within bounds
        if (transform.position.x > width)
        {
            transform.position = new Vector3(width ,0, transform.position.z);
        }
        else if(transform.position.x < (-1*width))
        {
            transform.position = new Vector3(width * -1, 0, transform.position.z);
        }
        if (transform.position.z > hheight)
        {
            transform.position = new Vector3(transform.position.x, 0, hheight);
        }
        else if (transform.position.z < lheight)
        {
            transform.position = new Vector3(transform.position.x, 0, lheight);
        }


        //Shoot out food
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Launch food
            Instantiate(projectilePrefab, new Vector3(transform.position.x, 1.5f, transform.position.z), projectilePrefab.transform.rotation);

        } 
    }
}
