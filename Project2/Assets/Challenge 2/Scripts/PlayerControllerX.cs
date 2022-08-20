using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float spawnSpeed = 4f;
    private float timeDelta = 0.0f;
    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && (timeDelta > spawnSpeed))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            timeDelta = 0.0f;
        }
        else
        {
            timeDelta = timeDelta + Time.deltaTime;
        }
    }
}
