using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] public float SpeedToMe;
    [SerializeField] public bool stopHere = false;
    [SerializeField] public float waitHereForSec;
    internal Vector3 PointPosition;

    private void Awake()
    {
       PointPosition = gameObject.transform.position;
    }
   
}
