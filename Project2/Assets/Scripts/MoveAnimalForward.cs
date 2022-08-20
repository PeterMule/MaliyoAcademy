using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimalForward : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 32.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Move animal along the path in the direction it is facing
        transform.Translate(Vector3.forward * Time.deltaTime * 0.6f * speed);
    }
}
