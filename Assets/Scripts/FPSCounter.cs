using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    int fps = 0;
    [SerializeField] List<Text> fpsTexts;

 

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = (int)(1.0f / deltaTime);

        foreach (Text fpsText in fpsTexts)
        {
            fpsText.text = "FPS: "+fps.ToString();
        }
    }
}
