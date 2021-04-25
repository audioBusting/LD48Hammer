using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    //public GameObject myCamera;
    public float flySpeed = 7;

    protected bool dead;

    public void onDeath()
    {
        //asdf
        dead = true;
        GetComponent<RawImage>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        //transform.SetParent(myCamera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead && transform.localScale.x < 7f)
        {
            float scaling = Time.deltaTime * flySpeed;
            transform.localScale += new Vector3(scaling,scaling,scaling);
        }
    }
}
