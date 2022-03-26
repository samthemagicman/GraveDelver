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

        //The Light gets more Red as levels progress
        light.color = new Color(0.7f, 0.75f - 0.05f * LevelDesigner.level, 0.2f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //The Light Shrinks as time goes on
        float timeLeft = StatController.totalTime - Time.timeSinceLevelLoad;

        float lightRadius = 200 + timeLeft;
        lightRadius = lightRadius / 120;

        light.pointLightOuterRadius = lightRadius;
        light.pointLightInnerRadius = lightRadius / 2;

        
        if ((timeLeft <= 10 && (int)(2*timeLeft) % 4 == 0) || timeLeft < 0.5)
        {
            light.pointLightOuterRadius = 0;
            light.pointLightInnerRadius = 0;
        }
    }
}

   
