using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;
	private float secCounter = 0;
	private int randomInt = Random.Range(3,6);

    private float startDelay = 1.0f;
    public float spawnInterval = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("SpawnRandomBall", startDelay, spawnInterval);
		InvokeRepeating("randomRangeInvoke", startDelay, 1.0f);
    }
	
	void randomRangeInvoke()
	{
		if(secCounter >= randomInt)
		{
            Debug.Log(randomInt);
			randomInt = Random.Range(3,6);
			secCounter = 0;
			SpawnRandomBall();
		}
		else
		{
			secCounter++;
		}
	}

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
        int ballIndex = Random.Range(0, ballPrefabs.Length);
        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
        //spawnInterval = Random.Range(3.0f, 5.1f);
    }

}
