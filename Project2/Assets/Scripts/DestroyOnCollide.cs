using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Food"))
        {
            Destroy(other.gameObject);
            if(health > 0)
            {
                health--;
            }
            else
            {
                Destroy(gameObject);
            }

        }

    }
}
