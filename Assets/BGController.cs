using BansheeGz.BGSpline.Curve;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGController : MonoBehaviour
{


    #region Refrences
    [SerializeField] BGCurve _BGCurve;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        if (!_BGCurve)
        {
            _BGCurve = GetComponent<BGCurve>();
        }
    }


  public void ChangeAnimation(List<Point> points) 
    {
        _BGCurve.Clear();
        int PointNumber = 0;
        foreach (var item in points)
        {
            print(PointNumber + "- " + item.PointPosition.x + "," + item.PointPosition.y + "," + item.PointPosition.z);
           var x = _BGCurve.AddPoint(new BGCurvePoint(_BGCurve, item.transform.position)); 
            
         
            //  print("bg - " + PointNumber + x.PointTransform.position.x + "," + x.PointTransform.position.y + "," + x.PointTransform.position.z);
            PointNumber++;
        }
    }

   
  
}
