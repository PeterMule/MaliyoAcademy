using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public bool seek = false;
    public GameObject target;
    private float speed = 12.0f;
    private float knockBackStrength = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(seek)
        {
            transform.LookAt(target.transform.position);
            transform.Rotate(Vector3.right * 90);
            //Vector3 heading = (target.transform.position - transform.position).normalized;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            //transform.Rotate(Vector3.up, 30 * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == target)
        {

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 knockOff = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(knockOff * knockBackStrength, ForceMode.Impulse);
            Debug.Log("Missile Collided with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
    public void SetKnockBackStrength(float newKBS)
    {
        knockBackStrength = newKBS;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
