using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{

    private float speed = 3.0f;
    private float spawnLimit = 2.0f;
    private int noMissiles = 2;
    private Rigidbody enemyRb;

    private GameObject player;
    public GameObject missileObj;
    public GameObject enemyPreFab;
    public GameObject powerupPreFab;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");
        InvokeRepeating("ShootMissiles", 7f, 10f);
        InvokeRepeating("SpawnEnemy", 10f, 12f);
        InvokeRepeating("SpawnPowerUp", 5f, 13f);


    }
    void Start(int difficulty)
    {
        enemyRb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");

        speed *= difficulty;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 heading = Vector3.Scale((player.transform.position - transform.position), new Vector3(1, 0, 1)).normalized;

        enemyRb.AddForce(heading * speed);

        if (FallenOfEdge())
        {
            Destroy(gameObject);
        }

    }
    private bool FallenOfEdge()
    {
        if (transform.position.y < -2.0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public float AddSpeed(float addition)
    {
        speed += addition;
        return speed;
    }

    private void ShootMissiles()
    {
        for (int i = 0; i < noMissiles; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 1.0f, Random.Range(-2.0f, 2.0f));
            GameObject m = Instantiate(missileObj, spawnPos, missileObj.transform.rotation);
            Missile ms = m.GetComponent<Missile>();
            ms.SetKnockBackStrength(20f);
            ms.target = player;
            ms.seek = true;
        }
    }
    void SpawnPowerUp()
    {
        Instantiate(powerupPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
    }
    void SpawnEnemy()
    {
        GameObject en = Instantiate(enemyPreFab, GenerateSpawnPoint(), enemyPreFab.transform.rotation);
        Enemy enem = en.GetComponent<Enemy>();
    }
    private Vector3 GenerateSpawnPoint()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-spawnLimit, spawnLimit),
                                                0,
                                                Random.Range(-spawnLimit, spawnLimit)) + transform.position;
        return spawnLocation;
    }
}

