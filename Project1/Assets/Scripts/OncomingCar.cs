using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncomingCar : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(0.0f, 18.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Move the vehicle forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed );
    }
}
