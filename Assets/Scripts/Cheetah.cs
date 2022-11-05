using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheetah : MonoBehaviour
{
    [Header("Hide cameras and hiding points")]
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

    [Header("Start of race")]
    [SerializeField] public Point StartRacePoint;
    [SerializeField] public Camera RunningCam;

    [Header("End of race")]
    [SerializeField] public Point EndOfRacePoint;

    [Header("Cheetah attributes")]
    [SerializeField] public Animator MyAnimator;
    [SerializeField] public float Speed;
    [SerializeField] public bool RunToBeginingOfRace = false;

    bool HidePhaseEnded;
   // bool NextPointIsEndOfRacePoint;
    Point NextPoint;
    int NextPointNum;
    int CurrentHidingCam;

    private void Awake() // add all "animations" to allAnim list
    {

        allAnims.Add(Animation1);
        allAnims.Add(Animation2);
        allAnims.Add(Animation3);
        allAnims.Add(Animation4);
        allAnims.Add(Animation5);
        allAnims.Add(Animation6);
        allAnims.Add(Animation7);
        allAnims.Add(Animation8);
    }

    public void TryToCatchCat() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (HidingCameras[CurrentHidingCam].WorldToViewportPoint(transform.position).x <=1.1 && HidingCameras[CurrentHidingCam].WorldToViewportPoint(transform.position).y <=1.1)
            {
                print("cat was visable");
            }
            else
            {
                print("missed the cat");
            }
            
            //check if cat is visable
            //if he is go to run screen
            //else print that the cat wasnt visable
        }
    }
    private void Start()  // get random camera , spawn cheetha in the first point of the new animation, start moving cheetha torwards the 2nd point 
    {
       
        NewCheetahLoc();
        CheetahSpawn();
        CheetahMove();
    }
    private void Update()
    {
       /* if (RunToBeginingOfRace)
        {
            ChangeNextPoint(StartRacePoint);
            RunToBeginingOfRace = false;
            HidePhaseEnded = true;
        }
        else if (NextPointIsEndOfRacePoint)
        {
            NextPointIsEndOfRacePoint = false;
            ChangeNextPoint(EndOfRacePoint);
        }*/

        Vector3 destination = Vector3.zero;

        if (!HidePhaseEnded)
        {
            destination = allAnims[CurrentHidingCam][NextPointNum].PointPosition;

        }
        else
        {
            destination = NextPoint.PointPosition;
        }

        Vector3 targetPos = new Vector3(destination.x, transform.position.y, destination.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
        TryToCatchCat();
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
        NextPointNum++;
        if (NextPointNum >= allAnims[CurrentHidingCam].Count)
        {

            NewCheetahLoc();
            CheetahSpawn();
            NextPointNum = 1;
        }
        Speed = allAnims[CurrentHidingCam][NextPointNum].SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);
        
        
        if (NextPointNum < Animation1.Count)
        {
           
            NextPoint = Animation1[NextPointNum];
            transform.LookAt(Animation1[NextPointNum].PointPosition);
        }
      
       
       
    }


    public void ChangeNextPoint(Point point)
    {
        Speed = point.SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);
        transform.LookAt(point.PointPosition);
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
        if (!HidePhaseEnded)
        {
           /* FinishLine fl;
            if (fl = other.GetComponent<FinishLine>())
            {
                fl.FinishRace();
            }
            else*/ if (other.gameObject.transform.position == allAnims[CurrentHidingCam][NextPointNum].PointPosition)
            {
                
                CheetahMove();
            }
        }
    }


}
