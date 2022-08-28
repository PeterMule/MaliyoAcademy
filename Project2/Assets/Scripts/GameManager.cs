using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{


    private int lives;
    private int score;
    private bool gameRunning;
    private TMP_Text textScore;
    private TMP_Text textLives;
    private TMP_Text textGameOver;

    // Start is called before the first frame update

    private void Start()
    {
        lives = 3;
        score = 0;
        gameRunning = true;

        textScore = FindTextbyTag("Score");
        writeScore();

        textLives = FindTextbyTag("Lives");
        writeLives();

        textGameOver = FindTextbyTag("GameOver");
        textGameOver.text = "";

    }
    void Start(int noLives)
    {
        if (noLives > 0 && noLives < 10)
        {
            lives = noLives;
        }
        else
        {
            lives = 3;
        }
        score = 0;
        gameRunning = true;

        textScore = FindTextbyTag("Score");
        textScore.text = "Score \n" + getScore();

        textLives = FindTextbyTag("Lives");
        textLives.text = "Lives \n" + getLives();

        textGameOver = FindTextbyTag("GameOver");
        textGameOver.text = "";
    }

    private TMP_Text FindTextbyTag(string tag)
    {
        GameObject finder = GameObject.FindGameObjectWithTag(tag);
        return finder.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameRunning)
        {
            textGameOver.text = "Game Over";
        }
    }
    public int getLives()
    {
        return lives;
    }
    public int getScore()
    {
        return score;
    }
    public bool getGameRunning()
    {
        return gameRunning;
    }
    private void writeLives()
    {
        string livestr = "";
        for (int i = 0; i < lives; livestr += "X", i++) ;
        textLives.text = "Lives \n" + livestr;
    }
    private void writeScore()
    {
        textScore.text = "Score \n" + score;
    }
    public void Addlives(int delLives)
    {
        if(gameRunning)
        {
            lives += delLives;
        }
        if(lives < 0)
        {
            Debug.Log("Game Over");
            gameRunning = false;
        }
        Debug.Log("Extra lives left  = " + lives);
        writeLives();

    }
    public void AddScore(int delScore)
    {
        if (gameRunning)
        {
            score += delScore;
        }
        if (lives < 0)
        {
            Debug.Log("Game Over");
            gameRunning = false;
        }
        Debug.Log("Score Earned  = " + score);
        writeScore();
    }
}
