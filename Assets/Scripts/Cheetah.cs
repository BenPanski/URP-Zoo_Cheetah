using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheetah : MonoBehaviour
{

    [SerializeField] public List<Camera> HidingCameras;
    [SerializeField] public List<Point> Animation1;
    [SerializeField] public List<Point> Animation2;
    [SerializeField] public List<Point> Animation3;
    [SerializeField] public List<Point> Animation4;
    [SerializeField] public List<Point> Animation5;
    [SerializeField] public List<Point> Animation6;
    [SerializeField] public List<Point> Animation7;
    [SerializeField] public List<Point> Animation8;

    List<List<Point>> allAnims = new List<List<Point>>();

    [SerializeField] public float Speed;
    [SerializeField] public Animator MyAnimator;

    int NextPoint;
    int CurrentHidingCam;

    private void Awake() // add all "animations" to allAnim list
    {

        allAnims.Add(Animation1);
        /*  allAnims.Add(Animation2);
          allAnims.Add(Animation3);
          allAnims.Add(Animation4);
          allAnims.Add(Animation5);
          allAnims.Add(Animation6);
          allAnims.Add(Animation7);
          allAnims.Add(Animation8);*/
    }
    private void Start()  // get random camera , spawn cheetha in the first point of the new animation, start moving cheetha torwards the 2nd point 
    {
        NewCheetahLoc();
        CheetahSpawn();
        CheetahMove();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, allAnims[CurrentHidingCam][NextPoint].PointPosition, Speed * Time.deltaTime);
    }

    /// <summary>
    /// set CurrentHidingCam to random camera
    /// </summary>
    public void NewCheetahLoc()
    {
        CurrentHidingCam = Random.Range(0, HidingCameras.Count);
       // print(CurrentHidingCam);
    }
    /// <summary>
    /// Set NextPoint, new speed & animation
    /// </summary>
    public void CheetahMove()
    {
        NextPoint++;
        if (NextPoint > allAnims[CurrentHidingCam].Count)
        {
            NextPoint = 1;
            NewCheetahLoc();
            CheetahSpawn();
        }
        Speed = allAnims[CurrentHidingCam][NextPoint].SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);
        transform.LookAt(Animation1[NextPoint].PointPosition);
    }
    /// <summary>
    /// Set Cheetah spawn to the new currentHidingCam in point 0
    /// </summary>
    public void CheetahSpawn()
    {
        transform.position = allAnims[CurrentHidingCam][0].PointPosition;
    }

    private void OnTriggerEnter(Collider other) // when cheetha collides with point call CheetahMove()
    {
        FinishLine fl;
        if (fl = other.GetComponent<FinishLine>())
        {
            fl.FinishRace();
        }
        else if (other.gameObject.transform.position == allAnims[CurrentHidingCam][NextPoint].PointPosition)
        {
            CheetahMove();
        }
    }


}
