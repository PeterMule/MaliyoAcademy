using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float speed = 5.0f;
    private Rigidbody enemyRb;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = Vector3.Scale((player.transform.position - transform.position),new Vector3(1,0,1)).normalized;

        enemyRb.AddForce(heading * speed);

        if(FallenOfEdge())
        {
            Destroy(gameObject);
        }

    }
    private bool FallenOfEdge()
    {
        if(transform.position.y < -2.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
