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
    [SerializeField] public List<Point> RunAnimation;
    List<List<Point>> allAnims = new List<List<Point>>();

    [Header("Start of race")]
    [SerializeField] public Point StartRacePoint;
    [SerializeField] public Camera RunningCam;
    [SerializeField] public GameObject RunningSpotLight;

    [Header("End of race")]
    [SerializeField] public Point EndOfRacePoint;

    [Header("Cheetah attributes")]
    [SerializeField] public Animator MyAnimator;
    [SerializeField] public float Speed;
    [SerializeField] public bool RunToBeginingOfRace = false;
    [SerializeField] bool SpawnOnlyInCam1;
    [SerializeField] bool OFIR_Y;

    bool HidePhaseEnded;
    // bool NextPointIsEndOfRacePoint;
    Point NextPoint;
    int NextPointNum;
    int CurrentHidingCam;
    float temp;
    [Header("Stop the cheetah")]
    [SerializeField] bool YouMayMove = true;

    private void OnEnable()
    {
        RunningSpotLight.SetActive(false);
    }

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
            if (HidingCameras[CurrentHidingCam].WorldToViewportPoint(transform.position).x <= 1.1 && HidingCameras[CurrentHidingCam].WorldToViewportPoint(transform.position).y <= 1.1)
            {
                print("cat was visable!");
                HidePhaseEnded = true;
                transform.position = RunAnimation[0].PointPosition;
                RunningSpotLight.SetActive(true);
                //gameObject.transform.LookAt(NextPoint.PointPosition);
                Speed = NextPoint.SpeedToMe;

            }
            else
            {
                print("missed the cat, you lost!");
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
        if (NextPointNum < allAnims[CurrentHidingCam].Count)
        {

            NextPoint = allAnims[CurrentHidingCam][NextPointNum];
            transform.LookAt(allAnims[CurrentHidingCam][NextPointNum].PointPosition);

        }
        if (Vector3.Distance(transform.position, RunAnimation[1].transform.position) < 1f)
        {
            print("won race");
            this.gameObject.SetActive(false);
        }

        if (YouMayMove)
        {
            Vector3 destination = Vector3.zero;

            if (!HidePhaseEnded)
            {
                destination = allAnims[CurrentHidingCam][NextPointNum].PointPosition;

            }
            else
            {
                destination = RunAnimation[1].PointPosition;
            }
            if (OFIR_Y)
            {
                temp = destination.y;
            }
            else
            {
                temp = transform.position.y;
            }
            Vector3 targetPos = new Vector3(destination.x, temp, destination.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
            //  transform.LookAt(Animation1[NextPointNum].PointPosition);
            /* if (NextPoint.PointPosition != allAnims[CurrentHidingCam][allAnims[CurrentHidingCam].Count -1].PointPosition)
             {
                 transform.LookAt(NextPoint.PointPosition);
             }*/
        }

        TryToCatchCat();
    }

    /// <summary>
    /// set CurrentHidingCam to random camera
    /// </summary>
    public void NewCheetahLoc()
    {
        if (SpawnOnlyInCam1)
        {
            print("SpawnOnlyInCam1 is true");
            CurrentHidingCam = 0;
        }
        else
        {

            int x = CurrentHidingCam;  // set x to current hiding cam
            while (x == CurrentHidingCam)
            {
                x = Random.Range(0, HidingCameras.Count);
            }

            CurrentHidingCam = x;
        }

    }
    /// <summary>
    /// Set NextPoint, new speed & animation
    /// </summary>
    public void CheetahMove()
    {
        if (HidePhaseEnded)
        {
            return;
        }
        NextPointNum++;
        if (NextPointNum >= allAnims[CurrentHidingCam].Count)
        {

            NewCheetahLoc();
            CheetahSpawn();
            NextPointNum = 1;
        }
        Speed = allAnims[CurrentHidingCam][NextPointNum].SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);




        print("next point is " + NextPointNum);
        print("current animation is " + (CurrentHidingCam + 1));

    }


    public void ChangeNextPoint(Point point)
    {
        Speed = point.SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);
        //transform.LookAt(point.PointPosition);
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
            if (other.gameObject.transform.position == allAnims[CurrentHidingCam][NextPointNum].PointPosition)
            {
                if (other.GetComponent<Point>().stopHere == true)
                {
                    YouMayMove = false;
                    Invoke("MayMove", (other.GetComponent<Point>().waitHereForSec));
                    MyAnimator.SetFloat("Speed", 0);

                }
                else
                {
                    CheetahMove();
                }
            }
        }

    }

    public void MayMove()
    {
        YouMayMove = true;
        MyAnimator.SetFloat("Speed", Speed);


    }
}
