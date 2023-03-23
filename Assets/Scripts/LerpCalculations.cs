using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LerpCalculations : MonoBehaviour
{
    [SerializeField] Transform cat;
    [SerializeField] float lerpDuration = 0.1f;
    [SerializeField] float distanceToStartLerp = 0.1f;
    float currentTime;


    [SerializeField] float CatForwardAngle = 20;


    [SerializeField] Vector3 Point1test;
    [SerializeField] Vector3 Point2test;
    [SerializeField] LerpData QuickTurn;
    [SerializeField] LerpData SlowTurn;


    void Update()
    {
        currentTime += Time.deltaTime;
    }

    public bool ShouldStartLerp(Vector3 NextPointPos, Vector3 PointAfterPos)
    {
        var x = AngleToLerpData(GetAngle(NextPointPos, PointAfterPos));

        if (Vector3.Distance(cat.position, NextPointPos) > x.LerpDistance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    

    public float GetAngle(Vector3 point1, Vector3 point2)
    {
        return -(Mathf.Atan2(point2.z - point1.z, point2.x - point1.x) * Mathf.Rad2Deg);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 rotatedVector = Quaternion.AngleAxis(270 + CatForwardAngle, Vector3.up) * cat.transform.right;
        Gizmos.DrawLine(cat.transform.position, cat.transform.position + rotatedVector);

        rotatedVector = Quaternion.AngleAxis(270 - CatForwardAngle, Vector3.up) * cat.transform.right;
        Gizmos.DrawLine(cat.transform.position, cat.transform.position + rotatedVector);


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Point1test, 1);

        var x = GetAngle(cat.transform.position, Point1test);
        print(x);

        if (AngleToLerpData(x) == QuickTurn)
        {
            Gizmos.color = Color.cyan;
        }
        else
        {
            Gizmos.color = Color.magenta;

        }
        rotatedVector = Quaternion.AngleAxis(x, Vector3.up) * cat.transform.right;
        Gizmos.DrawLine(cat.transform.position, cat.transform.position + rotatedVector);


    }
    public LerpData AngleToLerpData(float Angle)
    {
        float leftToCat = -90 - CatForwardAngle;
        float rightToCat = -90 + CatForwardAngle;

        if (Angle <= rightToCat && Angle >= leftToCat)
        {
            return SlowTurn;
        }
        else
        {
            return QuickTurn;
        }

    }

    [System.Serializable] public class LerpData 
    {
        public float LerpDistance;
        public float LerpTime;
    }
        
}
