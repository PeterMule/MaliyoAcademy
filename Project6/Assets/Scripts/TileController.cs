using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isColored = false;


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor(Color newcolor)
    {
        GetComponent<MeshRenderer>().material.color = newcolor;
        isColored = true;
    }
    public bool isChanged()
    {
        return isColored;
    }
}