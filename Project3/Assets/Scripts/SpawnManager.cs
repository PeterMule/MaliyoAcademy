using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] obstacles;
    
    private Vector3 spawnPos = new  Vector3(25, 0, 0);
    private Quaternion spawnrotation = Quaternion.Euler(0, 0, 0);
    private float spawnRate = 3.54f;
    private float spawnDelay = 5.34f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        //obstacles = new GameObject[3];
        //obstacles[0] = Resources.Load<GameObject>("Prefabs/Obstacle_1");
        //obstacles[1] = Resources.Load<GameObject>("Prefabs/Obstacle_2");
        //obstacles[2] = Resources.Load<GameObject>("Prefabs/Obstacle_3");
        //obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnObstacle", spawnDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            int rnd = Random.Range(0, obstacles.Length);

            Instantiate(obstacles[rnd], spawnPos, spawnrotation);
        }
    }
}
