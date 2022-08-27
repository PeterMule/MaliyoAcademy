using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update'
    public GameObject[] animalPrefabs;

    private float xRangeL = -19.0f;
    private float xRangeR = 19.0f;

    private float hheight = 30.0f;
    private float lheight = -10.0f;

    private float startDelay = 3.0f ;
    private float spawnInterval = 3.0f;
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Launch food
            int index = Random.Range(0, animalPrefabs.Length);
            Instantiate(animalPrefabs[index], new Vector3( Random.Range(-xRange, xRange),0.0f, zPos), animalPrefabs[index].transform.rotation);

        }
        */
    }
    void SpawnRandomAnimal()
    {
        int index = Random.Range(0, animalPrefabs.Length);
        switch(Random.Range(0,3))
        {
            //Spawn from Top
            case 0:
                {
                    Quaternion animalrotation = Quaternion.Euler(0, 180, 0);
                    Vector3 animalPos = new Vector3(Random.Range(xRangeL, xRangeR), 0.0f, hheight);
                    Instantiate(animalPrefabs[index], animalPos, animalrotation);
                    break;
                }
            //Spawn from Left
            case 1:
                {
                    Quaternion animalrotation = Quaternion.Euler(0, 90, 0);
                    Vector3 animalPos = new Vector3(xRangeL, 0.0f, Random.Range(lheight, hheight));
                    Instantiate(animalPrefabs[index], animalPos, animalrotation);
                    break;
                }
            //Spawn from Right
            case 2:
                {
                    Quaternion animalrotation = Quaternion.Euler(0,270,0);
                    Vector3 animalPos = new Vector3(xRangeR, 0.0f, Random.Range(lheight, hheight));
                    Instantiate(animalPrefabs[index], animalPos, animalrotation);
                    break;
                }
        }

    }
}
