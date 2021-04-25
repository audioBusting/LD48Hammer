using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SphereScript : MonoBehaviour
{
    public bool gameEnded = false;
    private bool animEnded;
    public float scaleSpeed = 1f;

    public UnityEvent OnEndAnimFinished;

    public void onGameEnd()
    {
        gameEnded = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        //gameEnded = false;
        animEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded && !animEnded)
        {
            float scaling = Time.deltaTime * scaleSpeed;
            transform.localScale += new Vector3(scaling, scaling, scaling);
            if (transform.localScale.x >= 28)
            {
                //asd
                OnEndAnimFinished.Invoke();
                animEnded = true;
            }
        }
    }
}
