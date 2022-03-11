using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchManager : MonoBehaviour
{
    Light2D light2d;
    void Start()
    {
        light2d = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        light2d.pointLightOuterRadius -= Time.deltaTime * 0.2f;
        if (light2d.pointLightOuterRadius < 0)
        {
            light2d.pointLightOuterRadius = 0;
        }
    }

    public void AddToRadius(float val)
    {
        light2d.pointLightOuterRadius += val;
    }

}
