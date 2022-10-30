using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public void SetColorGradient(Color topColor, Color bottomColor)
    {
        GetComponent<Renderer>().material.SetColor("_TopColor", topColor);
        GetComponent<Renderer>().material.SetColor("_BottomColor", bottomColor);
    }
}
