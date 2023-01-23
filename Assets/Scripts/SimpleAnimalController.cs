using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
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
    [SerializeField] List<Point> Anim13 = new List<Point>();
    [SerializeField] List<Point> Anim14 = new List<Point>();
    [SerializeField] List<Point> Anim15 = new List<Point>();
    [SerializeField] List<Point> Anim16 = new List<Point>();
    [SerializeField] List<Point> Anim17 = new List<Point>();
    [SerializeField] List<Point> Anim18 = new List<Point>();
    [SerializeField] List<Point> Anim19 = new List<Point>();
    [SerializeField] List<Point> Anim20 = new List<Point>();
    [SerializeField] List<Point> Anim21 = new List<Point>();

    #endregion

    List<List<Point>> AllAnims = new List<List<Point>>();
    int currentPointNum = 0;
    int currentAnimNum;
    List<Point> currentAnim;
    [SerializeField] bool printStuff;
    [SerializeField] bool SharesAnimations = true;
    List<SimpleAnimalController> AnimalsThatShareAnims = new List<SimpleAnimalController>();



    // Start is called before the first frame update
    void Start()
    {
        AddAllAnims();
        MoveMeToFirstPoint(GenerateUniqueRandomAnimNum());
        print(AnimalsThatShareAnims.Count);
    }
    private void Awake()
    {
        AddMeToAllAnimals();
    }

    private void AddMeToAllAnimals()
    {
        foreach (var item in FindObjectsOfType<SimpleAnimalController>())
        {
            if (item.SharesAnimations)
            {
                AnimalsThatShareAnims.Add(item);
            }
        }
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
        AddAnimation(Anim13);
        AddAnimation(Anim14);
        AddAnimation(Anim15);
        AddAnimation(Anim16);
        AddAnimation(Anim17);
        AddAnimation(Anim18);
        AddAnimation(Anim19);
        AddAnimation(Anim20);
        AddAnimation(Anim21);

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
        if (other.GetComponent<Point>() != null && other.transform.position == currentAnim[currentPointNum].transform.position)
        {
            currentPointNum += 1;
            if (currentPointNum <= currentAnim.Count - 1) // if not last point in animation
            {
                if (printStuff)
                {
                    print(gameObject.name + " is at point " + currentPointNum + " of Animation" + (currentAnimNum + 1));
                }
            }
            else // if last point in animation
            {
                if (printStuff)
                {
                    print(gameObject.name + " finished his animation, at point " + currentPointNum + " of Animation" + (currentAnimNum + 1));
                }
                MoveMeToFirstPoint(GenerateUniqueRandomAnimNum());
            }
        }
    }

    private void Update()
    {
        if (currentAnim.Count > currentPointNum)
        {
            transform.LookAt(currentAnim[currentPointNum].PointPosition);
            var dir = new Vector3(currentAnim[currentPointNum].PointPosition.x, transform.position.y, currentAnim[currentPointNum].PointPosition.z);
            transform.position = Vector3.MoveTowards(transform.position, dir, currentAnim[currentPointNum].SpeedToMe * Time.deltaTime);
        }
    }
    /*  int NewRandAnimNum()
      {
          int x = UnityEngine.Random.Range(0, AllAnims.Count);
          if (SharesAnimations) 
          {
              int q;
              foreach (var item in AnimalsThatShareAnims)
              {
                  if (item.currentAnimNum == x)
                  {
                      q =UnityEngine.Random.Range(0, AnimalsThatShareAnims.Count);
                  }
              }
          }
          if (printStuff)
          {
              print("next animation is: " + x);
          }
          return x;
      }*/
    public int GenerateUniqueRandomAnimNum()
    {
        List<int> NumbersList = GetAllAnimalsAnimNums();

        int randomNum = UnityEngine.Random.Range(0, AllAnims.Count);
        while (NumbersList.Contains(randomNum))
        {
            randomNum = UnityEngine.Random.Range(0, AllAnims.Count);
        }
        NumbersList.Add(randomNum);
        if (printStuff)
        {
            print("next animation is: " + randomNum);
        }
        return randomNum;
    }

    private List<int> GetAllAnimalsAnimNums()
    {
        var NumbersList = new List<int>();
        foreach (var item in AnimalsThatShareAnims)
        {
            NumbersList.Add(item.currentAnimNum);
        }

        return NumbersList;
    }

    void MoveMeToFirstPoint(int AnimationNumber)
    {
        if (RandomBoolean())
        {
            AllAnims[AnimationNumber].Reverse();
        }

        currentAnimNum = AnimationNumber;
        currentPointNum = 0;
        currentAnim = AllAnims[currentAnimNum];
        transform.position = AllAnims[currentAnimNum][currentPointNum].PointPosition;
    }
    bool RandomBoolean()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            return true;
        }
        return false;
    }

}