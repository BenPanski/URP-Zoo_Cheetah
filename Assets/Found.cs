using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    [SerializeField] Cheetah cheetah;
    List<Point> RunThroughPoints = new List<Point>();





    public Vector3 RunThroughHideScreens() 
    {
        for (int i = cheetah.CurrentHidingCam; i==0; i--)
        {
            
        }

        return Vector3.zero; 
    }
}
