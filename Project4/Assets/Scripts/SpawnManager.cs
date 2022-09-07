using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPreFab;
    public GameObject powerupPreFab;
    private float spawnLimit = 9.0f;
    private int enemiesToSpawn = 3;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnEnemy();
        //InvokeRepeating("SpawnEnemy", 0.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemies==0)
        {
            SpawnEnemyWave();
        }
    }
    void SpawnEnemyWave()
    {
        for(int i = 0; i< enemiesToSpawn;i++)
        {
            SpawnEnemy();
        }
        SpawnPowerUp();
        enemiesToSpawn++;
    }
    void SpawnPowerUp()
    {
        Instantiate(powerupPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
    }
    private Vector3 GenerateSpawnPoint()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnLimit, spawnLimit),
                                                0,
                                                Random.Range(-spawnLimit, spawnLimit));
        return spawnLocation;
    }
}
