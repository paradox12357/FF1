using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBehavior : MonoBehaviour
{
    Light myLight;
    bool lightingUp;
    float lightUp;
    void Start()
    {
        myLight = GetComponent<Light>();
        lightingUp = true;
        lightUp = 0;
    }

    void OnEnable()
    {
        lightingUp = true;
        lightUp = 0;
    }

    void Update()
    {
        //myLight.intensity = Mathf.PingPong(Time.time, 100f);
        if (lightingUp)
        {
            if(myLight.name.Contains("gemini"))
            {
                myLight.intensity = Mathf.Cos(lightUp) + 0.8f;
                lightUp++;
            }
            if(myLight.name.Contains("scorpio"))
            {
                myLight.intensity = Mathf.Cos(lightUp) + 0.8f;
                lightUp += 0.5f;
            }
            
        }
        if(lightUp > 100)
        {
            lightingUp = false;
        }
        //myLight.intensity = Mathf.Cos(Time.time * 10) + 0.8f;
        //myLight.intensity = Mathf.PingPong(10, 0.1f);
        //myLight.range = Mathf.PingPong(Time.time, 5f);
    }
}
