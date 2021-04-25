using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthCounterScript : MonoBehaviour
{
    public void OnHit(float depth)
    {
        curDepth += depth;
        depthText.text = string.Format("Depth: {0:0.0}", curDepth);
    }

    protected float curDepth;
    protected Text depthText;
    
    // Start is called before the first frame update
    void Start()
    {
        curDepth = 0;
        depthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
