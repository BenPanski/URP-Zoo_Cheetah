using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SimpleAnimalController : MonoBehaviour
{
    #region animation lists
    [SerializeField] List<Point> Anim1 = new List<Point>();
    [SerializeField] List<Point> Anim2 = new List<Point>();
    [SerializeField] List<Point> Anim3 = new List<Point>();
    [SerializeField] List<Point> Anim4 = new List<Point>();
    [SerializeField] List<Point> Anim5 = new List<Point>();
    [SerializeField] List<Point> Anim6 = new List<Point>();
    [SerializeField] List<Point> Anim7 = new List<Point>();
    [SerializeField] List<Point> Anim8 = new List<Point>();
    [SerializeField] List<Point> Anim9 = new List<Point>();
    [SerializeField] List<Point> Anim10 = new List<Point>();
    [SerializeField] List<Point> Anim11 = new List<Point>();
    [SerializeField] List<Point> Anim12 = new List<Point>();
    #endregion

    List<List<Point>> AllAnims = new List<List<Point>>();
    int currentPointNum = 0;
    int currentAnimNum;
    float Speed;

    List<Point> currentAnim;
    [SerializeField] bool PrintStuff = true;
    [SerializeField] bool YouMayMove = true;
    [SerializeField] Animator MyAnimator;






    // Start is called before the first frame update
    void Start()
    {
        AddAllAnims();
        MoveMeToFirstPoint(NewRandAnimNum());
    }
    #region AddAllAnims
    private void AddAllAnims()
    {
        AddAnimation(Anim1);
        AddAnimation(Anim2);
        AddAnimation(Anim3);
        AddAnimation(Anim4);
        AddAnimation(Anim5);
        AddAnimation(Anim6);
        AddAnimation(Anim7);
        AddAnimation(Anim8);
        AddAnimation(Anim9);
        AddAnimation(Anim10);
        AddAnimation(Anim11);
        AddAnimation(Anim12);
    }

    private void AddAnimation(List<Point> animation)
    {
        if (animation != null && animation.Any())
        {
            AllAnims.Add(animation);
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Point>() != null && other.gameObject.transform.position == AllAnims[currentAnimNum][currentPointNum].PointPosition)
        {
            currentPointNum += 1;
            if (currentPointNum < currentAnim.Count - 1) // if not last point in animation
            {
                if (PrintStuff)
                {
                    print(gameObject.name + " is at point " + currentPointNum + " of Animation" + currentAnimNum);
                }
                if (other.GetComponent<Point>().stopHere == true)
                {
                    YouMayMove = false;
                    Invoke("MayMove", (other.GetComponent<Point>().waitHereForSec));
                    UpdateAnimatorSpeed(0);

                }
            }
            else // if last point in animation
            {
                if (PrintStuff)
                {
                    print(gameObject.name + "finished his animation, at point " + currentPointNum + " of Animation" + currentAnimNum);
                }
                MoveMeToFirstPoint(NewRandAnimNum());
            }
        }
    }
    public void MayMove() // invoked ontriggerenter
    {
        YouMayMove = true;
        UpdateAnimatorSpeed();
    }
    private void UpdateAnimatorSpeed()
    {
        if (MyAnimator)
        {
            MyAnimator.SetFloat("Speed", Speed);
        }
        else
        {
            print("no animator connected to " + gameObject.name);
        }
    }
    private void UpdateAnimatorSpeed(int speed)
    {
        if (MyAnimator)
        {
            MyAnimator.SetFloat("Speed", speed);
        }
        else
        {
            print("no animator connected to " + gameObject.name);
        }
    }

    private void Update()
    {
        if (currentAnim.Count > currentPointNum && YouMayMove)
        {
            Speed = currentAnim[currentPointNum+1].SpeedToMe;
           // MyAnimator.SetFloat("Speed", Speed);
            transform.LookAt(currentAnim[currentPointNum + 1].PointPosition);
            transform.position = Vector3.MoveTowards(transform.position, currentAnim[currentPointNum + 1].PointPosition, currentAnim[currentPointNum].SpeedToMe * Time.deltaTime);
        }
    }
    int NewRandAnimNum()
    {
        return UnityEngine.Random.Range(0, AllAnims.Count);
    }

    void MoveMeToFirstPoint(int AnimationNumber)
    {
        currentAnimNum = AnimationNumber;
        currentPointNum = 0;
        currentAnim = AllAnims[currentAnimNum];
        transform.position = AllAnims[currentAnimNum][currentPointNum].PointPosition;
    }

}