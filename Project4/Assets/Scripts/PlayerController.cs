using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float powerUpTime = 7.0f;
    private float powerUpStrength = 50.9f;


    private Rigidbody playerRB;
    public GameObject powerupIndicator;

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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && powerActive)
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
        StartCoroutine(PowerupCountdownRoutine());
    }
}
