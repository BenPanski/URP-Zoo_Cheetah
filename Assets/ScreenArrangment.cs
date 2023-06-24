using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenArrangment : MonoBehaviour
{
    [SerializeField] List<RawImage> rawImages;
    List<int> ScreenOrder = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        LoadScreenOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
