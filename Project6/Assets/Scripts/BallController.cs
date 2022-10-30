using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public bool isTravelling;

    private Vector3 travelDir;
    private Vector3 nextCollisionPos;


    public int minSwipeRecognition = 500;
    private Vector2 swipePoslastframe;
    private Vector2 swipePosCurrentframe;
    private Vector2 currentSwipe;

    private Color solvecolor;

    // Start is called before the first frame update
    void Start()
    {
        solvecolor = Random.ColorHSV(0.5f, 1);
        GetComponent<MeshRenderer>().material.color = solvecolor;

        isTravelling = true;

        Invoke("UnsetTravelling", 2.5f);
    }
    void UnsetTravelling()
    {
        isTravelling = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(isTravelling)
        {
            rb.velocity = speed * travelDir;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);

        for(int i = 0; i < hitColliders.Length; i++)
        {
            TileController tile = hitColliders[i].transform.GetComponent<TileController>();
            if(tile && !tile.isColored)
            {
                tile.ChangeColor(solvecolor);
            }
            
        }

        if(nextCollisionPos != Vector3.zero)
        {
            if(Vector3.Distance(transform.position, nextCollisionPos) < 0.5f)
            {
                isTravelling = false;
                travelDir = Vector3.zero;
                nextCollisionPos = Vector3.zero;
            }
        }
        if (isTravelling)
            return;
        if(Input.GetMouseButton(0))
        {
            swipePosCurrentframe = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if(swipePoslastframe != Vector2.zero)
            {
                currentSwipe = swipePosCurrentframe - swipePoslastframe;

                if(currentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                currentSwipe.Normalize();
                //Up/Down
                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5)
                {
                    //go up/down
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }
                if (currentSwipe.y > -0.5f && currentSwipe.y < 0.5)
                {
                    //go left/right
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }
            swipePoslastframe = swipePosCurrentframe;
        }

        if(Input.GetMouseButtonUp(0))
        {
            swipePoslastframe = Vector2.zero;
            currentSwipe = Vector2.zero;
        }
    }
    private void SetDestination(Vector3 direction)
    {
        travelDir = direction;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollisionPos = hit.point;
        }
        isTravelling = true;
    }
}
