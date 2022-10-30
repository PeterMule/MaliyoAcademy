using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public GameObject tilePrefab;
    public GameObject wallPrefab;
    public GameObject ballPrefab;

    public Text levelIndicator;
    public BackGroundController bgController;

    public int tempGridSize = 6;
    public ulong tempseed = 1152921504606846975;

    private bool checkGamespace = true;
    private BallController currentBall;
    private GameObject[,] theGrid;
    private GameObject[] theWalls;
    //private ulong[] levels = { 2147077824256, 2139652923136, 1030820462593 , 8824568861952, 9620726898904, 10720238526680, 115291535146975, 142180821767432, 127378263040, 125260013568, 2132604489281, 124124684881436, 123164915572480 };
    private ulong[] levels = { 2147077824256, 2139652923136, 1030820462593, 8824568861952, 9620726898904, 142180821767432, 2132604489281, 124124684881436, 123164915572480 };
    private int[] levelSizes = { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7 };
    private int currentLevel;
    private static Vector3 ballStartPos = new Vector3(-3f, 0.5f, 0f);


    private Vector3 offset = new Vector3(-3, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        CleanGameSpace();
        currentLevel = -1;
        //PlaceBall();
        //InitializeGrid(7, 1030820462593);
        //Debug.Log(offset);
        //LoadLevel(currentLevel); 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentLevel = (currentLevel + 1) % levels.Length;
            LoadLevel(currentLevel); 
        }
        if(!currentBall.isTravelling && checkGamespace)
        {
            checkGamespace = false;
            if(IsFullyColored())
            {
                checkGamespace = false;
                currentLevel = (currentLevel + 1) % levels.Length;
                LoadLevel(currentLevel);
            }
        }
        else if(currentBall.isTravelling)
        {
            checkGamespace = true;
        }
        //CleanGameSpace();
        //InitializeGrid(tempGridSize, tempseed);
    }
    public void ReloadLevel()
    {
        LoadLevel(currentLevel);
    }
    private void LoadLevel(int levelIndex)
    {
        if(levelIndex < levels.Length)
        {
            CleanGameSpace();
            InitializeGrid(levelSizes[levelIndex], levels[levelIndex]);
            WriteLevel(levelIndex);
            PlaceBall();
            ChangeBackground();
        }
    }
    GameObject CreateTile(Vector3 pos)
    {
        GameObject newTile = Instantiate(tilePrefab, pos + offset, tilePrefab.transform.rotation);
        return newTile;
    }
    GameObject CreateWall(Vector3 pos, Vector3 scale)
    {
        GameObject newWall = Instantiate(wallPrefab, pos - Vector3.up + offset, wallPrefab.transform.rotation);
        newWall.transform.localScale = scale;
        return newWall;
    }
    void InitializeGrid(int gridSize = 7, ulong seed = 155864)
    {
        theGrid = new GameObject[gridSize, gridSize];
        theWalls = new GameObject[4];
        
        //Debug.Log("int is of size " + sizeof(ulong));
        //Walls
        //W-Left
        theWalls[0] = CreateWall((new Vector3(-1, 0.5f, gridSize / 2f) ) , new Vector3(1, 1, gridSize + 1));
        //W-Bottom
        theWalls[1] = CreateWall((new Vector3((gridSize / 2f) - 1, 0.5f, -1)) , new Vector3(gridSize + 1, 1, 1));
        //W-Right
        theWalls[2] = CreateWall((new Vector3(gridSize, 0.5f, (gridSize / 2f) - 1)) , new Vector3(1, 1, gridSize + 1));
        //W-Left
        theWalls[3] = CreateWall((new Vector3(gridSize / 2f, 0.5f, gridSize)) , new Vector3(gridSize + 1, 1, 1));

        bool[] binSeed = dec2bin(seed, gridSize * gridSize);

        Debug.Log(bin2string(binSeed));
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                
                if (binSeed[(i + (gridSize * j)) >= binSeed.Length ? (i + (gridSize * j)) % binSeed.Length : (i + (gridSize * j))])
                {
                    Debug.Log(i + (gridSize * j));
                    theGrid[i, j] = CreateWall((new Vector3(i, 0.5f, j)) , new Vector3(1, 1, 1));
                }
                else
                {
                    theGrid[i, j] = CreateTile((new Vector3(i, 0, j)));
                }

            }
        }


    }
    void CleanGameSpace()
    {
        GameObject[] oldTiles = GameObject.FindGameObjectsWithTag("Tile");
        for (int i = 0; i < oldTiles.Length; Destroy(oldTiles[i]), i++) ;
        GameObject[] oldWalls = GameObject.FindGameObjectsWithTag("Wall");
        for (int i = 0; i < oldWalls.Length; Destroy(oldWalls[i]), i++) ;
        GameObject[] oldBalls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < oldBalls.Length; Destroy(oldBalls[i]), i++) ;
    }
    void PlaceBall()
    {
        GameObject[] oldBalls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < oldBalls.Length; Destroy(oldBalls[i]), i++) ;
        GameObject ab = Instantiate(ballPrefab, ballStartPos, ballPrefab.transform.rotation);
        currentBall = ab.GetComponent<BallController>();
    }
    void ChangeBackground()
    {
        bgController.SetColorGradient(Random.ColorHSV(0.5f, 1), Random.ColorHSV(0.5f, 1));
    }
    void WriteLevel(int levelIndex)
    {
        levelIndicator.text = "Level " + (levelIndex+1).ToString();
    }
    bool[] dec2bin(ulong num, int gridSiz = 49)
    {
        bool[] bini = new bool[64];
        int i = 0;
        while (num > 0)
        {
            bini[i++] = num % 2 > 0 ? true : false;
            num /= 2;
        }
        
        bool[] revbini = new bool[64];
        for(int ii = gridSiz-1,j=0 ; ii >= 0 ; ii--,j++)
        {
            revbini[ii] = bini[j]==true;
        }
        return revbini;
    }
    string bin2string(bool[] bin)
    {
        string a = "";
        for (int i = 0; i < bin.Length; a += (bin[i++]) ? "1" : "0") ;
        return a;
    }
    bool IsFullyColored()
    {
        int gridSize = (int)(Mathf.Sqrt(theGrid.Length));
        Debug.Log("grid size is " + gridSize.ToString());
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                
                if (theGrid[i, j].CompareTag("Wall"))
                {
                    ;
                }
                else
                {
                    if(!theGrid[i, j].GetComponent<TileController>().isColored)
                    {
                        return false;
                    }
                }

            }
        }
        return true;
    }
}
