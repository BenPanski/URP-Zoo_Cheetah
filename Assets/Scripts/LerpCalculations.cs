using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LerpCalculations : MonoBehaviour
{
    [SerializeField] Transform Animal;
    [SerializeField] float AnimalForwardAngle = 20;
    [SerializeField] Vector3 Point1test;
    [SerializeField] LerpData QuickTurn;
    [SerializeField] LerpData SlowTurn;
    
    public float currentTime;

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    public bool ShouldStartLerp(Vector3 NextPointPos, Vector3 PointAfterPos)
    {
        var x = AngleToLerpData(GetAngle(NextPointPos, PointAfterPos));
        float distance = Vector3.Distance(Animal.position, NextPointPos);
        if (distance <= x.LerpDistance)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ResetCurrentTime()
    {
        currentTime = 0;
    }

    public float GetAngle(Vector3 point1, Vector3 point2)
    {
        return -(Mathf.Atan2(point2.z - point1.z, point2.x - point1.x) * Mathf.Rad2Deg);
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 rotatedVector = Quaternion.AngleAxis(270 + AnimalForwardAngle, Vector3.up) * Animal.transform.right;
        Gizmos.DrawLine(Animal.transform.position, Animal.transform.position + rotatedVector);

        rotatedVector = Quaternion.AngleAxis(270 - AnimalForwardAngle, Vector3.up) * Animal.transform.right;
        Gizmos.DrawLine(Animal.transform.position, Animal.transform.position + rotatedVector);


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Point1test, 1);

        var x = GetAngle(Animal.transform.position, Point1test);

        if (AngleToLerpData(x) == QuickTurn)
        {
            Gizmos.color = Color.cyan;
        }
        else
        {
            Gizmos.color = Color.magenta;

        }
        rotatedVector = Quaternion.AngleAxis(x, Vector3.up) * Animal.transform.right;
        Gizmos.DrawLine(Animal.transform.position, Animal.transform.position + rotatedVector);


    }
    public LerpData AngleToLerpData(float Angle)
    {
        float leftToCat = -90 - AnimalForwardAngle;
        float rightToCat = -90 + AnimalForwardAngle;

        if (Angle <= rightToCat && Angle >= leftToCat)
        {
            return SlowTurn;
        }
        else
        {
            return QuickTurn;
        }

    }

    internal void LerpAnimal(Vector3 pointPosition1, Vector3 pointPosition2)
    {

        LerpData lerpData = AngleToLerpData(GetAngle(pointPosition1, pointPosition2));

        float  NextRotationAngle = GetAngle(Animal.transform.position,pointPosition2)+90;


        float CatAngle = Animal.transform.eulerAngles.y;

        Animal.transform.rotation = Quaternion.Lerp(Animal.transform.rotation, Quaternion.Euler(0, NextRotationAngle, 0), lerpData.LerpCurve.Evaluate(currentTime / lerpData.LerpDuration));
    }

    [System.Serializable] public class LerpData 
    {
        public float LerpDistance;
        public float LerpDuration;
        public AnimationCurve LerpCurve;
    }
        
}
