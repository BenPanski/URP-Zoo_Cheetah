using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.Random;

enum CatState
{
    Hide, Hunt, RunScreen,
}
public class Cheetah : MonoBehaviour
{
    #region REF
    [Header("Refrences")]
    [SerializeField] GameManager _GameManger;
    #endregion
    #region Hide Cams & Points
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
    #endregion
    #region Cheetah attributes
    [Header("Cheetah attributes")]
    [SerializeField] public Animator MyAnimator;
    [SerializeField] public float Speed;
    [SerializeField] public bool RunToBeginingOfRace = false;
    #endregion
    #region private variables
    bool HidePhaseEnded;
    // bool NextPointIsEndOfRacePoint;
    Point NextPoint;
    int NextPointNum;
    int CurrentHidingCam;
    float temp;
    #endregion
    #region Tests
    [Header("Tests")]
    [SerializeField] bool YouMayMove = true;
    [SerializeField] bool SpawnOnlyInCam1;
    [SerializeField] CatState MyState;
    [SerializeField] bool OFIR_Y;
    [SerializeField] bool MoveThroughAnimsInOrder = false;
    [SerializeField] bool WorldSpeedTimes5 = false;
    [SerializeField] bool PrintAnimNum = true;
    [SerializeField] bool PrintPointNum = true;
    [SerializeField] bool NoHuntPhase = false;
    #endregion

    #region Init
    private void Awake() // add all "animations" to allAnim list , // get random camera , spawn cheetha in the first point of the new animation, start moving cheetha torwards the 2nd point 
    {
        MyState = CatState.Hide;
        InitListsOfPoints();

        print("press space to try and catch the cheetah");
        print("press f to simulate the players finishing the race");
        InitCheetahLoc();
        MoveCatToFirstPoint();
        CheetahMove();

        if (WorldSpeedTimes5)
        {
            print("WorldSpeedTimes5");
            Time.timeScale = 5f;
        }
    }
   
    private void InitListsOfPoints()
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
    #endregion
    public void TryToCatchCat()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MyState == CatState.Hide)
        {
            if (HidingCameras[CurrentHidingCam].WorldToViewportPoint(transform.position).x <= 1.1 && HidingCameras[CurrentHidingCam].WorldToViewportPoint(transform.position).y <= 1.1) // if cat is visble 
            {
                print("cat was visable!");
                MyState = CatState.Hunt;

                if (NoHuntPhase)
                {
                    SetRunScreenState();
                }
                else
                {
                    SetHuntScreenState();
                }
            }
            else
            {
                print("missed the cat, you lost!  (no code for this yet)");
            }
        }
    }

    private void SetRunScreenState()
    {
        MyState = CatState.RunScreen;

        print("cat is in run screen state");
        transform.position = RunAnimation[0].PointPosition;
        NextPoint = RunAnimation[1];
        Speed = NextPoint.SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);
        transform.LookAt(NextPoint.PointPosition);
    }
    private void SetHuntScreenState()
    {
        MyState = CatState.Hunt;

        print("cat is in hunt state");
         // set transform
         // set next point
         // set speed
         // set animator
         //transform.lookAt
       
    }

   

    private void Update()
    {
        if (YouMayMove)
        {
            CatMovement(); // move to destination
        }

        if (NextPointNum < allAnims[CurrentHidingCam].Count && MyState == CatState.Hide) // if in hide phase and not at the end of animation
        {
            NextPoint = allAnims[CurrentHidingCam][NextPointNum];
            transform.LookAt(allAnims[CurrentHidingCam][NextPointNum].PointPosition);

        }
        if (MyState == CatState.RunScreen && Vector3.Distance(transform.position, RunAnimation[1].transform.position) < 2f ) // if cat is at end of run animation
        {
            _GameManger.CatWon();
            print("Cheetah finished race, turning cheetah off");
            this.gameObject.SetActive(false);
        }

        TryToCatchCat();
    }

    private void CatMovement()
    {
        Vector3 destination = SetCatDestination();
        Vector3 targetPos = new Vector3(destination.x, temp, destination.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private Vector3 SetCatDestination()
    {
        Vector3 destination = Vector3.zero; // reset destination

        if (MyState == CatState.Hide)
        {
            destination = allAnims[CurrentHidingCam][NextPointNum].PointPosition; // set destination to next point

        }
        else
        {
            destination = RunAnimation[1].PointPosition;// set destination to run point
        }
        if (OFIR_Y)
        {
            temp = destination.y; // set destination  y
        }
        else
        {
            temp = transform.position.y;// change destination y to current y
        }

        return destination;
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
        else if (MoveThroughAnimsInOrder)
        {
            CurrentHidingCam++;
            if (CurrentHidingCam >= allAnims.Count)
            {
                SetRunScreenState();
            }
        }
        else
        {
            RandomizeHideCam();
        }
    }
    private void InitCheetahLoc()
    {
        if (SpawnOnlyInCam1)
        {
            print("SpawnOnlyInCam1 is true");
            CurrentHidingCam = 0;
        }
        else if (MoveThroughAnimsInOrder)
        {
            print("MoveThroughAnimsInOrder");
            CurrentHidingCam = 0;
        }
        else
        {
            RandomizeHideCam();
        }
    }

    public void RandomizeHideCam()
    {
        int x = CurrentHidingCam;  // set x to current hiding cam
        while (x == CurrentHidingCam)
        {
            x = Random.Range(0, HidingCameras.Count);
        }
        CurrentHidingCam = x;
    }
    /// <summary>
    /// Set NextPoint, new speed & animation
    /// </summary>
    public void CheetahMove()
    {
        if (MyState == CatState.RunScreen)
        {
            return;
        }

        NextPointNum++;
        if (NextPointNum >= allAnims[CurrentHidingCam].Count) // if at the end of current animation
        {

            NewCheetahLoc();
            MoveCatToFirstPoint();
            NextPointNum = 1;
        }
        Speed = allAnims[CurrentHidingCam][NextPointNum].SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);

        if (PrintAnimNum) { print("current animation is " + (CurrentHidingCam + 1)); }
        if (PrintPointNum) { print("next point is " + NextPointNum); }

    }

    /// <summary>
    /// Set Cheetah spawn to the new currentHidingCam in point 0
    /// </summary>
    public void MoveCatToFirstPoint()
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

    public void MayMove() // invoked ontriggerenter
    {
        YouMayMove = true;
        MyAnimator.SetFloat("Speed", Speed);
    }
}
