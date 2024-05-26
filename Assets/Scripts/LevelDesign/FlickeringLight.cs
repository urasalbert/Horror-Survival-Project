using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light LightObject;

    float interval = 1;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            LightObject.enabled = !LightObject.enabled;
            interval = Random.Range(0f, 1f);
            timer = 0;
        }
    }
}
