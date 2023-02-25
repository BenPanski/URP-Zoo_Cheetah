using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChangeRezOnInput : MonoBehaviour
{
    [SerializeField] List<RawImage> Images;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            print("normal rez");
            foreach (var item in Images)
            {
                RenderTexture renderTexture = item.texture as RenderTexture;
                renderTexture.Release(); // Release the current Render Texture
                renderTexture.width = 1024;
                renderTexture.height = 512;
                renderTexture.Create();


            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            print(" rez *2");
            foreach (var item in Images)
            {
                RenderTexture renderTexture = item.texture as RenderTexture;
                renderTexture.Release(); // Release the current Render Texture
                renderTexture.width = 2048;
                renderTexture.height = 1024;

                renderTexture.Create();

            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            print(" rez *4");
            foreach (var item in Images)
            {
                RenderTexture renderTexture = item.texture as RenderTexture;
                renderTexture.Release(); // Release the current Render Texture
                renderTexture.width = 4096;
                renderTexture.height = 2048;

                renderTexture.Create();

            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            print(" rez *8");
            foreach (var item in Images)
            {
                RenderTexture renderTexture = item.texture as RenderTexture;
                renderTexture.Release(); // Release the current Render Texture
                renderTexture.width = 8192;
                renderTexture.height = 4096;

                renderTexture.Create();

            }
        }
    }
}
