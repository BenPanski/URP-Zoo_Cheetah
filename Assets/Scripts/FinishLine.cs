using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] Point startingPoint;
    [SerializeField] bool raceFinished;
    [SerializeField] Cheetah cheetah;

    float CheethaDis;
    float RaceLength;
    private void Start()
    {
        RaceLength = Vector3.Distance(transform.position, startingPoint.transform.position);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FinishRace();
        }
    }
    public void FinishRace()
    {
        CalculateCheetahDis();
    }

    private void CalculateCheetahDis()
    {
        CheethaDis = Vector3.Distance(cheetah.transform.position, startingPoint.transform.position);
        print(CheethaDis * 100 / RaceLength);
    }

   
}
