using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerSpin : MonoBehaviour
{
    // Start is called before the first frame update
    private float rotationSpeed = 2000.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f,  rotationSpeed * Time.deltaTime);
    }
}
