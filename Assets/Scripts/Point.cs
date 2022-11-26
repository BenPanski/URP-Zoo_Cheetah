using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] public float SpeedToMe;
    [SerializeField] public bool stopHere = false;
    [Range(1, 50)]
    [SerializeField] public float waitHereForSec;
    internal Vector3 PointPosition;

    private void Awake()
    {
        PointPosition = gameObject.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stopHere)
        {
            if (other.GetComponent<Cheetah>() != null)
            {
                
                Invoke("DontStop", 1f);
                Invoke("StopHere", waitHereForSec*2);

            }
        }
    }

    public void DontStop()
    {
        
            stopHere = false;
           
        

    }
    public void StopHere()
    {

        stopHere = true;



    }
}


