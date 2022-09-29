using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] public float SpeedToMe;
    internal Vector3 PointPosition;

    private void Awake()
    {
       PointPosition = gameObject.transform.position;
    }
   
}
