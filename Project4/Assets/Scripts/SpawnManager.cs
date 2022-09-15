using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPreFab;
    public GameObject bossEnemyPreFab;
    public GameObject powerupPreFab;

    public Text levelIndicator;
    private float spawnLimit = 9.0f;
    private int enemiesToSpawn = 1;
    private int level = 1;

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
        WriteLevel();
        if (level%2 != 0)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
            SpawnPowerUp();
        }
        else
        {
            SpawnBoss();
        }
        enemiesToSpawn++;
        level++;
    }
    void SpawnPowerUp()
    {
        Instantiate(powerupPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
    }
    void SpawnBoss()
    {
        Instantiate(bossEnemyPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
    }
    void SpawnEnemy()
    {
        GameObject en = Instantiate(enemyPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
        Enemy enem = en.GetComponent<Enemy>();
        enem.AddSpeed((level - 1f) * 0.3f);
    }
    private Vector3 GenerateSpawnPoint()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnLimit, spawnLimit),
                                                0,
                                                Random.Range(-spawnLimit, spawnLimit));
        return spawnLocation;
    }
    private void WriteLevel()
    {
        levelIndicator.text = "Level " + level;
    }
}
