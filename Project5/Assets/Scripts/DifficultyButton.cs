using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button buttonObj;
    private GameManager gameManagerObj;
    public float difficulty;
    // Start is called before the first frame update
    void Start()
    {
        buttonObj = GetComponent<Button>();
        buttonObj.onClick.AddListener(SetDifficulty);

        gameManagerObj = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetDifficulty()
    {
        Debug.Log("The " + gameObject.name + " was clicked");
        gameManagerObj.StartGame(difficulty);
        GameObject[] titlescreen = GameObject.FindGameObjectsWithTag("TitleScreen");
        for (int i = 0; i < titlescreen.Length; titlescreen[i].SetActive(false), i++) ;
    }
}
