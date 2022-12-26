using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    [SerializeField] Cheetah cheetah;
    List<Point> RunThroughPoints = new List<Point>(); //9 points, first point is hide cam 8, last point is hide cam 1,run cam is the 9th point





    private void Update()
    {

        if (cheetah.CurrentHidingCam == 0)
        { print(8); }
        else
        {
            print(cheetah.CurrentHidingCam - 1);
        }
    }



    
}
