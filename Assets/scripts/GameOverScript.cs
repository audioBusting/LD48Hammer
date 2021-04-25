using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject myCamera;
    public float flySpeed;

    protected bool dead;

    public void onDeath()
    {
        //asdf
        dead = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        transform.SetParent(myCamera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead && transform.position.z > 2f)
        {
            transform.Translate(Vector3.back * Time.deltaTime * flySpeed, Space.World);
        }
    }
}
