using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightController : MonoBehaviour
{

    Light2D light;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float lightRadius = 200 + StatController.totalTime - (Time.timeSinceLevelLoad);
        lightRadius = lightRadius / 120;
            
        light.pointLightOuterRadius = lightRadius;
        light.pointLightInnerRadius = lightRadius / 2;
    }
}
