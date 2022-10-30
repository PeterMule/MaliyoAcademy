using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool inPosition = false;
    private float slideSpeed = 1.3f;
    private float correctY = 0.5f;
    private float deltaY = 1f;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - deltaY, transform.position.z);
        inPosition = false;
        slideSpeed += Random.value * 5f;
    }

    // Update is called once per frame
    void Update()
    {
        SlideIn();
    }
/*    IEnumerator SlideIntoView()
    {

        SlideIn();
    }
    IEnumerator SlideOutofView()
    {
        inPosition = false;
        correctY = transform.position.y-deltaY;

    }*/
    public void SlideIn()
    {
        if(!inPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (slideSpeed * Time.deltaTime), transform.position.z);
            if(transform.position.y >= correctY)
            {
                inPosition = true;
                transform.position = new Vector3(transform.position.x, correctY, transform.position.z);
            }
        }
    }
    public void SlideOut()
    {
        if (!inPosition)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (slideSpeed * Time.deltaTime), transform.position.z);
            if (transform.position.y <= correctY)
            {
                inPosition = true;
            }
        }
    }
    private void OnDestroy()
    {
        SlideOut();
    }
    public bool isChanged()
    {
        return false;
    }
}
