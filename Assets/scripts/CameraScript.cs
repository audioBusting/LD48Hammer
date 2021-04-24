using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //minimum depth before i start moving
    public float minDepth;

    private float depthBuffer;
    private bool startedMoving;
    private Vector3 targetPosition; 

    public void OnHammerHit(float hitDepth)
    {
        depthBuffer += hitDepth;
        if (startedMoving)
        {
            targetPosition = targetPosition - new Vector3 (0,hitDepth,0);
        }
        else if (depthBuffer > minDepth)
        {
            startedMoving = true;
            hitDepth = depthBuffer - minDepth;
            targetPosition = targetPosition - new Vector3 (0,hitDepth,0);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        depthBuffer=0f;
        targetPosition = transform.position;
        startedMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, 
                targetPosition, 
                Time.deltaTime);
    }
}
