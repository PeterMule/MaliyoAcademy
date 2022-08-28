using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class CollisionHandler : MonoBehaviour
{ 
    private GameManager gameManager;
    //private TMP_Text score;
    //private TMP_Text lives;
    //private TMP_Text gameOver;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        /*
        score = FindTextbyTag("Score");
        score.text = "Score \n" + gameManager.getScore();

        lives = FindTextbyTag("Lives");
        lives.text = "Lives \n" + gameManager.getLives();

        gameOver = FindTextbyTag("GameOver");
        gameOver.text = "";
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag + " : " + gameObject.tag);


        //Food has collided with an animal
        if (other.CompareTag("Food") && gameObject.CompareTag("Animal"))
        {
            Destroy(other.gameObject);
            gameObject.GetComponent<AnimalHunger>().FeedAnimal(1);
            //score.text = "Score \n" + gameManager.getScore();
        }
        //An Animal has collided with the Player
        else if (gameObject.CompareTag("Player") && other.CompareTag("Animal"))
        {
            
            Destroy(other.gameObject);
            if (gameManager.getLives() > 0 )
            {
                gameManager.Addlives(-1);
                //lives.text = "Lives \n" + gameManager.getLives();
            }
            else
            {
                //gameOver.text = "Game Over";
                gameManager.Addlives(-10);
                Destroy(gameObject);
            }

        }

    }
}
