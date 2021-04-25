using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class _UnityEventFloat : UnityEvent<float> {}

public class HammerScript : MonoBehaviour
{
    [Header("When the hammer hits! passes how deep it hits")]
    public _UnityEventFloat hammerHit;

    public UnityEvent hammerStart;

    protected enum HammerStates
    {
        INIT, HOLD, SWINGBACK, HOLDUP, SWINGING, HITSTOP, DEAD
    }
    protected HammerStates hammerState;
    public Vector3 relativeRotationPoint;
    protected Vector3 absoluteRotationPoint;
    public float swingSpeed;
    public float backswingSpeed;
    public float backswingMax;
    protected float swingCounter;
    protected float swingCounterPeak;
    protected Vector3 startPos;

    public float hitstopLength;
    protected float hitstopCounter;

    //how deep can each hit nail in
    public float maxHitDepth;

    protected float rotateAmt; //idk man rotation amount for this frame idc

    //on nail death, stop hammer
    public void onDeath()
    {
        hammerState = HammerStates.DEAD;
    }
    // Start is called before the first frame update
    void Start()
    {
        hammerState = HammerStates.INIT;
        absoluteRotationPoint = transform.position + relativeRotationPoint;
        swingCounter = 0f;
        hitstopCounter = 0f;

        if (hammerHit == null) hammerHit = new _UnityEventFloat();
        if (hammerStart == null) hammerStart = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        switch(hammerState)
        {
            case HammerStates.INIT:
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                hammerState = HammerStates.HOLD;
                Debug.Log("going hold mode");
                hammerStart.Invoke();
            }
            break;

            case HammerStates.HOLD:
            if(Input.GetKeyUp(KeyCode.Space)) 
            {
                startPos = transform.position;
                hammerState = HammerStates.SWINGBACK;
                Debug.Log("going swingback mode");
            }
            break;

            case HammerStates.SWINGBACK:
            //rotate back
            rotateAmt = Time.deltaTime*backswingSpeed;
            swingCounter += rotateAmt;
            gameObject.transform.RotateAround(
                absoluteRotationPoint, 
                Vector3.back, rotateAmt);

            //swing if key down
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                if (swingCounter < 0f) swingCounter = 0; //double check so it doesn't go up
                hammerState = HammerStates.SWINGING;
                swingCounterPeak = swingCounter;
                Debug.Log("going swinging");
            }

            //hold if too far up
            if (swingCounter > backswingMax)
            {
                hammerState = HammerStates.HOLDUP;
                Debug.Log("holding hammer up");
            }
            break;

            case HammerStates.HOLDUP:
            //swing if key down (copied from backswing)
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (swingCounter < 0f) swingCounter = 0; //double check so it doesn't go up
                hammerState = HammerStates.SWINGING;
                swingCounterPeak = swingCounter;
                Debug.Log("going swinging");
            }
            break;

            case HammerStates.SWINGING:
            //swing down
            rotateAmt = Time.deltaTime*swingSpeed;
            swingCounter -= rotateAmt;
            gameObject.transform.RotateAround(
                absoluteRotationPoint, 
                Vector3.forward, rotateAmt);
            //Debug.Log(gameObject.transform.eulerAngles.x);
            
            //check if slammed down
            if (swingCounter <= 0f)
            {
                //zeroing stuff out to be safe
                swingCounter = 0f;
                gameObject.transform.eulerAngles = new Vector3(0, 90, 270);
                hammerState = HammerStates.HITSTOP;
                
                //doing the slamma
                float hitDepth = swingCounterPeak/backswingMax*maxHitDepth;
                Debug.Log("hitting down by " + hitDepth);
                gameObject.transform.position =  startPos - new Vector3(0,hitDepth,0);
                hammerHit.Invoke(hitDepth);
                absoluteRotationPoint = transform.position + relativeRotationPoint;
                Debug.Log("going hitstop");
            }
            break;

            case HammerStates.HITSTOP:
            //one frame hitstop?
            hitstopCounter += Time.deltaTime;
            if (hitstopCounter >= hitstopLength)
            {
                hitstopCounter = 0f;
                hammerState = HammerStates.HOLD;
            }
            break;

            case HammerStates.DEAD:
            //don't move
            if(Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            break;

        }
    }
}
