using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScreenNum : MonoBehaviour
{
    [SerializeField] List<GameObject> ScreenNums;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (var item in ScreenNums)
            {
                item.SetActive(!item.activeSelf);
            }
        }
    }
}
