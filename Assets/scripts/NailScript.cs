using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NailScript : MonoBehaviour
{
    public UnityEvent nailDeath;

    public float hpMax;
    public float hpRegen;
    private float hpCur;
    private bool dead;

    public void OnHammerHit(float hitDepth)
    {
        // Debug.Log("nail moving down by " + hitDepth);
        gameObject.transform.Translate(Vector3.down* hitDepth, Space.World);
        
        //take damage
        hpCur -= hitDepth;
        //check if dead
        if (hpCur <= 0)
        {
            //TODO die
            nailDeath.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hpCur = hpMax;
        dead = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dead) return;
        
        //regenerate HP
        hpCur += hpRegen*Time.deltaTime;
        if (hpCur > hpMax) hpCur = hpMax;

        float colorRatio = hpCur/hpMax;
        //TODO do a red overlay or something?
        GetComponent<Renderer>().material.SetColor("_Color", new Color(1f,colorRatio,colorRatio));

        if (hpCur < 0)
        {
            nailDeath.Invoke();
            dead = true;
        }
    }
}
