using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(-11 % 12);
        transform.position = new Vector3(transform.position.x % 13f, 0f, transform.position.z % 13f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * 24);
        
    }
}
