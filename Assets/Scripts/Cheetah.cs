using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] GameManager _GameManager;
    [SerializeField] LineRenderer CatLineDrawer;
    [SerializeField] LerpCalculations _LerpCalculations;

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
    List<List<Point>> allAnims = new List<List<Point>>();
    [SerializeField] public List<Point> RunAnimation;
    List<Point> HuntAnimation = new List<Point>();
    [Range(0, 10)][SerializeField] float HuntSpeed; // more then 10 causes problems
    #endregion
    #region Cheetah attributes
    [Header("Cheetah attributes")]
    [SerializeField] public Animator MyAnimator;
    [SerializeField] public float Speed;
    [SerializeField] public bool RunToBeginingOfRace = false;
    Point pointOnCat;
    #endregion
    #region private variables
    bool HidePhaseEnded;
    // bool NextPointIsEndOfRacePoint;
    Point NextPoint;
    int NextPointNum;
    int CurrentHidingCam;
    float temp;
    float LastTeleportTime = 0f;
    float TeleportInterval = 0.2f;
    #endregion
    #region Tests
    [Header("Tests")]
    [SerializeField] bool YouMayMove = true;
    [SerializeField] bool SpawnOnlyInCam1;
    [SerializeField] bool Spawn6to8AndGoDown;
    [SerializeField] CatState MyState;
    [SerializeField] bool OFIR_Y;
    [SerializeField] bool MoveThroughAnimsInOrder = false;
    [SerializeField] bool MoveThroughAnimsInReverseOrder = false;
    [SerializeField] bool WorldSpeedTimes5 = false;
    [SerializeField] bool PrintAnimNum = true;
    [SerializeField] bool PrintPointNum = true;
    [SerializeField] bool NoHuntPhase = false;
    bool LerpInitFlag = false;

    #endregion

    #region Init
    private void Awake() // add all "animations" to allAnim list , // get random camera , spawn cheetha in the first point of the new animation, start moving cheetha torwards the 2nd point 
    {
        NullRefCheck();

        MyState = CatState.Hide;
        InitListsOfHidePoints();

        print("press space to try and catch the cheetah");
        print("press f to simulate the players finishing the race");
        InitCheetahLoc();
        MoveCatToFirstHidePoint();
        CheetahMove();
        WorldSpeedChange();
        InitLineDrawer();
        try
        {
            pointOnCat = GetComponent<Point>();
        }
        catch (System.Exception)
        {
            print("cheetah is doesnt have a point on it");
            throw;
        }
        
       
    }
   /* private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((allAnims[CurrentHidingCam][1].PointPosition),1);
    }*/
    private void NullRefCheck()
    {
        if (!_GameManager)
        {
            _GameManager = FindObjectOfType<GameManager>();
        }
        if (!CatLineDrawer)
        {
            CatLineDrawer = FindObjectOfType<LineRenderer>();
        }
    }

    private void InitLineDrawer() 
    {
        int x = 0;
        foreach (var item in allAnims)
        {
            x += item.Count;
        }
        CatLineDrawer.positionCount = x;

        int y = 0;
        foreach (var anim in allAnims)
        {
            foreach (var point in anim)
            {
                CatLineDrawer.SetPosition(y, point.PointPosition);
                y++;
            }
        }
    }
    private void InitHuntAnim()
    {
        List<List<Point>> HuntPList = allAnims.GetRange(0, allAnims.Count);
        HuntAnimation = new List<Point>();
        for (int i = CurrentHidingCam; i >= 0; i--)
        {
            var item = HuntPList[i];
            HuntAnimation.Add(item[0]);
            HuntAnimation.Add(item[item.Count - 1]);
        }
        HuntAnimation.RemoveAt(0);



    }

    private void WorldSpeedChange()
    {
        if (WorldSpeedTimes5)
        {
            print("WorldSpeedTimes5");
            Time.timeScale = 5f;
        }
    }

    private void InitListsOfHidePoints()
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
    public void TryToCatchCat()    // Itay - this method is called when the first sensor is triggered
    {
        if (MyState == CatState.Hide) {
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

    #region SetState
    private void SetRunScreenState()
    {
        MyState = CatState.RunScreen;

        print("cat is in run screen state");
        transform.position = RunAnimation[0].PointPosition;
        NextPoint = RunAnimation[1];
        NextPointNum = 1;
        Speed = NextPoint.SpeedToMe;
        MyAnimator.SetFloat("Speed", Speed);
       // transform.LookAt(NextPoint.PointPosition);
    }
    private void SetHuntScreenState()
    {
        InitHuntAnim();
        MyState = CatState.Hunt;

        print("cat is in hunt state");
        NextPointNum = 0;
        NextPoint = HuntAnimation[0];
        Speed = HuntSpeed;
        MyAnimator.SetFloat("Speed", HuntSpeed);
       // transform.LookAt(NextPoint.PointPosition);
    }
    #endregion

    #region Movement & destination
    private void Update()
    {
        if (YouMayMove)
        {
            CatMovement(); // move to destination
            CatRotation();// rotates to destination
        }


        switch (MyState)
        {
            case CatState.Hide:
                if (NextPointNum < allAnims[CurrentHidingCam].Count)
                {
                    NextPoint = allAnims[CurrentHidingCam][NextPointNum];
                   //transform.LookAt(allAnims[CurrentHidingCam][NextPointNum].PointPosition);
                }
                break;
            case CatState.Hunt:
                if (NextPointNum < HuntAnimation.Count)
                {
                    NextPoint = HuntAnimation[NextPointNum];
                   // transform.LookAt(HuntAnimation[NextPointNum].PointPosition);
                }
                else if (Vector3.Distance(transform.position, HuntAnimation[HuntAnimation.Count - 1].transform.position) < 3f)
                {
                    print("finished hunt phase");
                    SetRunScreenState();
                }
                break;
            case CatState.RunScreen:
                if (Vector3.Distance(transform.position, RunAnimation[1].transform.position) < 2f)
                {
                    _GameManager.CatWon();
                    print("Cheetah finished race, turning cheetah off");
                    this.gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
        TryToCatchCat();
        }
    }

    private void CatMovement()
    {
        Vector3 destination = SetCatDestination();
        Vector3 targetPos = new Vector3(destination.x, temp, destination.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }
    private void CatRotation()
    {
        List<Point> TempPointList = null; 
        
        switch (MyState)
        {
            case CatState.Hide:
                if (NextPointNum >= allAnims[CurrentHidingCam].Count - 1)
                {
                    return;
                }
                TempPointList = allAnims[CurrentHidingCam];
                break;
            case CatState.Hunt:
               
                TempPointList = HuntAnimation;
                transform.LookAt(TempPointList[NextPointNum].PointPosition);
                return;
               


            case CatState.RunScreen:

                transform.LookAt(RunAnimation[1].transform.position);
               return;
            default:
                print("error in cat rotation");
                break;
        }
       
       
       

        
        if (_LerpCalculations.ShouldStartLerp(TempPointList[NextPointNum].PointPosition, TempPointList[NextPointNum+1].PointPosition))
        {
            if (!LerpInitFlag)
            {
                _LerpCalculations.ResetCurrentTime();
                LerpInitFlag = true;
            }
            
            _LerpCalculations.LerpAnimal(TempPointList[NextPointNum].PointPosition, TempPointList[NextPointNum + 1].PointPosition);

        }
        else
        {
            LerpInitFlag = false;
            _LerpCalculations.ResetCurrentTime();
            //transform.LookAt(allAnims[CurrentHidingCam][NextPointNum].PointPosition);
        }

        
    }
    private Vector3 SetCatDestination()
    {
        Vector3 destination = Vector3.zero; // reset destination
        switch (MyState)
        {
            case CatState.Hide:
                try
                {
                    destination = allAnims[CurrentHidingCam][NextPointNum].PointPosition; // set destination to next point
                }
                catch (System.Exception)
                {
                    print("error in set cat destination cam " + CurrentHidingCam + " point " + NextPointNum + "");
                    throw;
                }
                ;
                break;
            case CatState.Hunt:
                destination = HuntAnimation[NextPointNum].PointPosition;
                // todo set destination
                break;
            case CatState.RunScreen:
                destination = RunAnimation[1].PointPosition;// set destination to run point
                break;
            default:
                print("cat has no state");
                break;
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
    #endregion


    /// <summary>
    /// set CurrentHidingCam to random camera
    /// </summary>
    public void SetHideCam()
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
        else if (MoveThroughAnimsInReverseOrder)
        {
            CurrentHidingCam--;
            if (CurrentHidingCam >= allAnims.Count)
            {
                SetRunScreenState();
            }
        }
        else if (Spawn6to8AndGoDown)
        {
            
            
            if (CurrentHidingCam >= 2)
            {
                CurrentHidingCam -= Random.Range(1, 3);
            }
            else if (CurrentHidingCam == 1)
            {
                CurrentHidingCam -= 1;
            }
            else
            {
                SetHuntScreenState();
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
        else if (MoveThroughAnimsInReverseOrder)
        {
            print("MoveThroughAnimsInOrder");
            CurrentHidingCam = 8;
        }
        else if (Spawn6to8AndGoDown)
        {

            CurrentHidingCam = Random.Range(6, 9);
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
        
        _LerpCalculations.ResetCurrentTime();
        NextPointNum++;
        
        if (MyState == CatState.Hide && NextPointNum >= allAnims[CurrentHidingCam].Count) // if at the end of hiding animation
        {
            SetHideCam();
            MoveCatToFirstHidePoint();
            NextPointNum = 1;
        }
        if (MyState == CatState.Hunt && NextPointNum >= HuntAnimation.Count)// if at the end of hunt animation
        {
            SetRunScreenState();
        }
        if (MyState == CatState.Hide)
        {
            Speed = allAnims[CurrentHidingCam][NextPointNum].SpeedToMe;
            if (PrintAnimNum)
            {
                print("current animation is " + (CurrentHidingCam + 1));
                MyAnimator.SetFloat("Speed", Speed);
            }
        }


        if (PrintPointNum) { print("next point is " + NextPointNum); }

    }

    /// <summary>
    /// Set Cheetah spawn to the new currentHidingCam in point 0
    /// </summary>
    public void MoveCatToFirstHidePoint()
    {
        try
        {
            var x = allAnims[CurrentHidingCam][0].PointPosition;
        }
        catch (System.Exception)
        {
            CurrentHidingCam--;

        }

        transform.position = allAnims[CurrentHidingCam][0].PointPosition;
        this.transform.LookAt(allAnims[CurrentHidingCam][1].PointPosition);  // look at first point in animation
    }

    private void OnTriggerEnter(Collider other) // when cheetha collides with point call CheetahMove()
    {
        if (MyState == CatState.Hunt)  // if in hunt phase
        {

            if (other.gameObject.transform.position == HuntAnimation[HuntAnimation.Count - 1].PointPosition)
            {
                SetRunScreenState();
            }
            else if (other.gameObject.transform.position == HuntAnimation[NextPointNum].PointPosition)
            {

                float currentTime = Time.time;
               /* if (currentTime - LastTeleportTime >= TeleportInterval)
                {*/
                    CheetahMove();
                    if (NextPointNum % 2 != 0)
                    {
                        print("teleport cat");
                        transform.position = HuntAnimation[NextPointNum].PointPosition;
                    }
                    LastTeleportTime = currentTime;
              //  }
            }
        }
        else if (MyState == CatState.Hide)
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
