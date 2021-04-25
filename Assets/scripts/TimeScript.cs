using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{

    protected float timeCounter;
    protected bool dead;
    protected bool started;
    protected Text timeText;
    protected float frameCounter; //ui updates too fast if i don't throttle it

    public void onDeath()
    {
        dead = true;
        timeText.text = string.Format("Time: {0:0.000}s", timeCounter);
    }

    public void onStart()
    {
        started = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        started = false;
        timeCounter = 0f;
        timeText = GetComponent<Text>();
        frameCounter = 0.001f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && started)
        {
            timeCounter += Time.deltaTime;
            frameCounter -= Time.deltaTime;
            if (frameCounter < 0f)
            {
                //Debug.Log(string.Format("Time: {0:0.000}s", timeCounter));
                frameCounter = 0.002f;
                timeText.text = string.Format("Time: {0:0.000}s", timeCounter);
            }
        }
    }
}
