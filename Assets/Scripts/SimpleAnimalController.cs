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
    List<Point> currentAnim;



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
        if (other.GetComponent<Point>() != null)
        {
            currentPointNum++; 
            if (currentPointNum < currentAnim.Count -1) // if not last point in animation
            {
                print(gameObject.name + " is at point " + currentPointNum + " of Animation" + (currentAnimNum + 1));
            }
            else // if last point in animation
            {
                print(gameObject.name + " finished his animation, at point " + currentPointNum + " of Animation" + (currentAnimNum + 1));
                MoveMeToFirstPoint(NewRandAnimNum());
            }
        }
    }

    private void Update()
    {
        if (currentAnim.Count > currentPointNum)
        {
            transform.LookAt(currentAnim[currentPointNum].PointPosition);
            transform.position = Vector3.MoveTowards(transform.position, currentAnim[currentPointNum].PointPosition, currentAnim[currentPointNum].SpeedToMe * Time.deltaTime);
        }
    }
    int NewRandAnimNum()
    { 
        var x = UnityEngine.Random.Range(0, AllAnims.Count);
        print("next animation is: "+ x);
        return x;
    }

    void MoveMeToFirstPoint(int AnimationNumber)
    {
        currentAnimNum = AnimationNumber;
        currentPointNum = 0;
        currentAnim = AllAnims[currentAnimNum];
        transform.position = AllAnims[currentAnimNum][currentPointNum].PointPosition;
    }

}