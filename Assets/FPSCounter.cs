using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] List<Text> FpsCounters;
    // Start is called before the first frame update
    float frameCount = 0;
    float nextUpdate = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    void Start()
    {
        nextUpdate = Time.time;
    }

    void Update()
    {
        frameCount++;
        if (Time.time > nextUpdate)
        {
            nextUpdate += 1.0f / updateRate;
            fps = frameCount * updateRate;
            frameCount = 0;
        }
        foreach (var item in FpsCounters)
        {
            item.text = "FPS: " + fps.ToString("F2");
        }
    }
}
