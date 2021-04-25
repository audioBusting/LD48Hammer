using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NailScript : MonoBehaviour
{
    public UnityEvent nailDeath;
    [Header("when the nail hits the ball")]
    public UnityEvent gameEnd;

    public float hpMax;
    public float hpRegen;
    public float regenCooldown;
    private float hpCur;
    private float regenCur;
    private bool dead;

    public void OnHammerHit(float hitDepth)
    {
        // Debug.Log("nail moving down by " + hitDepth);
        gameObject.transform.Translate(Vector3.down* hitDepth, Space.World);
        
        //take damage
        hpCur -= hitDepth;
        regenCur = regenCooldown;
        //check if dead
        if (hpCur <= 0)
        {
            //TODO die
            GetComponent<AudioSource>().Play();
            nailDeath.Invoke();
        }
        
        if (transform.position.y <= -135)
        {
            gameEnd.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hpCur = hpMax;
        dead = false;
        regenCur = regenCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead) return;

        //regenerate HP
        regenCur -= Time.deltaTime;
        if (regenCur <= 0f)
        {
            hpCur += hpRegen*Time.deltaTime;
            if (hpCur > hpMax) hpCur = hpMax;
        }

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
