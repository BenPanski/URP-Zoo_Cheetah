using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenArrangment : MonoBehaviour
{
    [SerializeField] List<RenderTexture> textures;
    [SerializeField] List<RawImage> rawImages;
    List<int> ScreenOrder = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        EnableMultiScreen();

        LoadScreenOrder();
        ArrangeScreens();
    }

    private static void EnableMultiScreen()
    {
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }



    void LoadScreenOrder()
    {
        string[] order_file = File.ReadAllLines(Application.streamingAssetsPath + "/screen_order.ini");
        for (int i = 0; i < order_file.Length; i++)
        {
            if (order_file[i] != "")
            {
                ScreenOrder.Add(int.Parse(order_file[i]));
            }
        }
    }
    void ArrangeScreens()
    {
        print("amount of numbers in screenorder "+ScreenOrder.Count);
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < rawImages.Count; i++)
        {
            positions.Add(rawImages[i].transform.position);
        }
        for (int i = 0; i < rawImages.Count; i++)
        {
            //print("raw image num "+i+" "+rawImages[i].texture.name);
            //print("texture num " + i + " " + textures[ScreenOrder[i]]);
            //rawImages[i].texture = textures[ScreenOrder[i] - 1];
            rawImages[i].transform.position = positions[ScreenOrder[i] - 1];
        }
    }

}
