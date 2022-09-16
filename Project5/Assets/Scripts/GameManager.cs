using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public List<int> points;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public GameObject pauseScreen;
    public Button restartButton;
    public Slider volumeSlider;

    private int score = 0;
    private int noLives = 0;

    private float spawnRate = 1.2f;
    private bool isGameActive = false;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
    }

    IEnumerator SpawnTargets()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnRate);
            CreateTarget(1);
        }
    }
    void ResetScore()
    {
        score = 0;
        isGameActive = true;
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        WriteScore();
    }
    public void IncreaseScore(int increase = 0)
    {
        if(isGameActive)
        {
            score += increase;

            WriteScore();
        }

    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public bool GameActive()
    {
        return isGameActive;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LoadTitleScreen();
    }
    void WriteScore()
    {
        scoreText.text = "Score: " + score;
    }
    void WriteLives()
    {
        livesText.text = "Lives: " + noLives;
    }
    void CreateTarget(int num = 1)
    {
        if(isGameActive)
        {
            for (int i = 0; i < num; i++)
            {
                int rnd = Random.Range(0, targets.Count);
                Target trgt = Instantiate(targets[rnd]).GetComponent<Target>();
                trgt.Points(points[rnd]);
            }
        }

    }
    public void StartGame(float sR=1)
    {
        isPaused = false;
        spawnRate /= sR;
        StartCoroutine(SpawnTargets());
        ResetScore();
        noLives = 3;
        WriteLives();
    }
    public void LossLife()
    {
        noLives--;
        if(noLives < 1)
        {
            GameOver();
            WriteLives();
        }
        else
        {
            WriteLives();
        }
        
    }
    public void LoadTitleScreen()
    {
        GameObject[] titlescreen = GameObject.FindGameObjectsWithTag("TitleScreen");
        for (int i = 0; i < titlescreen.Length; titlescreen[i].SetActive(true), i++) ;
    }
    void PauseMenu()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        isPaused = !isPaused;
    }
    public void VolumeChanged()
    {
        Debug.Log(volumeSlider.value);
    }
}
