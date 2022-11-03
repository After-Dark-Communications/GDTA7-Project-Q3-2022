using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLigth : MonoBehaviour
{
    private const float minIntervalTime = 20f;
    private const float maxIntervalTime = 60f;

    private Light light;

    private float intervalTreshhold = 0f;
    private float currentInterval = 0f;

    void Start()
    {
        light = GetComponent<Light>();
        SetRandomInterval();
    }


    void Update()
    {
        currentInterval += Time.deltaTime;

        if (currentInterval < intervalTreshhold)
            return;
        
        SetRandomInterval();

        light.enabled = !light.enabled;
    }

    private void SetRandomInterval()
    {
        currentInterval = 0;
        intervalTreshhold = UnityEngine.Random.Range(minIntervalTime, maxIntervalTime);
    }
}
