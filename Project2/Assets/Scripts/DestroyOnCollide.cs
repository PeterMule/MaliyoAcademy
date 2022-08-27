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
        //Food has collided with an animal
        if (other.CompareTag("Food")&& gameObject.CompareTag("Animal"))
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
        //An Animal has collided with the Player
        else if (gameObject.CompareTag("Player") && other.CompareTag("Animal"))
        {
            Destroy(other.gameObject);
            if (health > 0)
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
