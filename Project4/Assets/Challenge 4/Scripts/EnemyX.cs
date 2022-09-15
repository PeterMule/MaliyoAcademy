using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyX : MonoBehaviour
{
    public float speed = 12;

    public Text homeText;
    public Text awayText;

    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private static int playerGoals = 0;
    private static int enemyGoals = 0;



    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        speed = 12;
        playerGoal = GameObject.Find("Player Goal");
        homeText = GameObject.Find("HomeText").GetComponent<Text>();
        awayText = GameObject.Find("AwayText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            IncreasePlayerGoals();
            Destroy(gameObject);
            

        } 
        else if (other.gameObject.name == "Player Goal")
        {
            IncreaseEnemyGoals();
            Destroy(gameObject);
            
            
        }

    }
    void IncreasePlayerGoals()
    {
        playerGoals++;
        Debug.Log("Player goals are " + playerGoals);
        homeText.text = "Home\n" + playerGoals;
    }
    void IncreaseEnemyGoals()
    {
        enemyGoals++;
        Debug.Log("Enemy goals are " + enemyGoals);
        awayText.text = "Away\n" + enemyGoals;
    }
}
