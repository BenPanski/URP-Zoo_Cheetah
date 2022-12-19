using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    [SerializeField] Cheetah cheetah;
    List<Point> RunThroughPoints = new List<Point>();





    public void RunThroughHideScreens() 
    {
        for (int i = cheetah.CurrentHidingCam; i==0; i--)
        {
            
        }
    }
}
