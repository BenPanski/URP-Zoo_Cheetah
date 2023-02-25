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
    public LayerMask layerMask;
    internal Vector3 PointPosition;
     float raycastDistance = 40f;
    private void Awake()
    {

        // Shoot a raycast downwards from this object's position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, layerMask))
        {
            // Change the Y position of this object's transform to be 7 units above the hit point
            transform.position = new Vector3(hit.point.x, hit.point.y , hit.point.z);//+ 6.936f
        }
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


