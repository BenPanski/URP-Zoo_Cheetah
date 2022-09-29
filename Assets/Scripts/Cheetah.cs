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

    private void Awake()
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
    private void Start()
    {
        NewCheetahLoc();
        CheetahSpawn();
        CheetahMove();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, allAnims[CurrentHidingCam][NextPoint].PointPosition, Speed * Time.deltaTime);

    }

    public void NewCheetahLoc()
    {
        CurrentHidingCam = Random.Range(0, HidingCameras.Count);
        print(CurrentHidingCam);
    }

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

    public void CheetahSpawn()
    {
        transform.position = allAnims[CurrentHidingCam][0].PointPosition;
    }

    public void UpdateSpeed()
    {
        MyAnimator.SetFloat("Speed", Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.position == allAnims[CurrentHidingCam][NextPoint].PointPosition)
        {
            CheetahMove();
        }
    }


}
