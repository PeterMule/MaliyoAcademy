using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float powerUpTime = 7.0f;
    private float powerUpStrength = 50.9f;
    private float pushBackStrength = 50.9f;
    

    private int powerUpType;
    //0 => KnockBack
    //1 => Missiles
    //2 => PushAway

    private Rigidbody playerRB;
    public GameObject powerupIndicator;
    public GameObject missileObj;
    public Text PowerUpTextBox;

    public bool powerActive = false;
    private Vector3 playerSpawn = new Vector3(0f, 2.2f, 0f);

    
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerSpawn;
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float vInput = Input.GetAxis("Vertical");

        playerRB.AddForce(focalPoint.transform.forward * vInput * speed);

        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = playerSpawn;
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }
        if(powerActive)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.7f, 0);
            powerupIndicator.transform.Rotate(Vector3.up, Time.deltaTime * 36);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                switch (powerUpType)
                {
                    //KnockBack
                    case 0:
                        {
                            //pass
                            break;
                        }
                    //Missiles
                    case 1:
                        {
                            ShootMissiles();
                            powerUpType = 3;
                            WritePowerUp();
                            powerActive = false;
                            powerupIndicator.SetActive(false);
                            break;
                        }
                    //PushBack
                    case 2:
                        {
                            PushBackEnemies();
                            powerUpType = 3;
                            WritePowerUp();
                            powerActive = false;
                            powerupIndicator.SetActive(false);
                            break;
                        }
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            ActivatePowerUp();
            Destroy(other.gameObject);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpTime);
        powerActive = false;
        powerupIndicator.SetActive(false);
        powerUpType = 3;
        WritePowerUp();
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
        for (int i = 0; i < missiles.Length; i++)
        {
            Destroy(missiles[i]);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && powerActive && powerUpType==0)
        {

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 knockOff = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(knockOff * powerUpStrength,ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " while powerUp is active");
            //Destroy(collision.gameObject);
        }
    }
    void ActivatePowerUp()
    {
        powerActive = true;
        powerupIndicator.SetActive(true);
        powerUpType = Random.Range(0, 3);
        switch(powerUpType)
        {
            //KnockBack
            case 0:
                {
                    StartCoroutine(PowerupCountdownRoutine());
                    break;
                }
            //Missiles
            case 1:
                {
                    break;
                }
            //PushBack
            case 2:
                {

                    break;
                }
        }
        WritePowerUp();
    }
    void WritePowerUp()
    {
        switch (powerUpType)
        {
            //KnockBack
            case 0:
                {
                    PowerUpTextBox.text = "KnockBack";
                    break;
                }
            //Missiles
            case 1:
                {
                    PowerUpTextBox.text = "Missiles";
                    break;
                }
            //PushBack
            case 2:
                {
                    PowerUpTextBox.text = "PushBack";
                    break;
                }
            default:
                {
                    PowerUpTextBox.text = "";
                    break;
                }
        }
    }

    void ShootMissiles()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i< enemies.Length; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 1.0f, Random.Range(-2.0f, 2.0f));
            GameObject m = Instantiate(missileObj, spawnPos, missileObj.transform.rotation);
            Missile ms = m.GetComponent<Missile>();
            ms.target = enemies[i];
            ms.seek = true;
        }
    }
    void PushBackEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Vector3 distance = Vector3.Scale(new Vector3(1, 0, 1), (enemies[i].transform.position - transform.position));

            Rigidbody enemyRb = enemies[i].GetComponent<Rigidbody>();
            enemyRb.AddForce(enemyRb.mass * 2 * (1/distance.magnitude) * distance.normalized *(pushBackStrength), ForceMode.Impulse);
        }
    }
}
