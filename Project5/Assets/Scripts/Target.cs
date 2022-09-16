using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticles;

    private Rigidbody targetRb;
    private GameManager gameManager;


    private int points;
    private float[] speeds = { 12f, 16f };
    private float maxTorque = 10f;
    private float xRange = 4;
    private float ySpawnPos = -2.6f;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);

        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());

        transform.position = RandomSpawnPos();


        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManager.GameActive())
        {
            gameManager.IncreaseScore(points);
        }
        Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad") && gameManager.GameActive())
        {
            gameManager.LossLife();
        }
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(speeds[0], speeds[1]);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    public void Points(int newpoints)
    {
        points = newpoints;
    }
}
