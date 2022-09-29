using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheetah : MonoBehaviour
{

    [SerializeField] public List<Camera> list;
    [SerializeField] public List<Transform> CatPlacement;

    float count;





    private void Update()
    {
        count +=Time.deltaTime;

        if (count > 10)
        {
            NewRandomCatPlacment();
            count = 0;
        }
        
    }

    public void NewRandomCatPlacment() 
    {
        var rnd = Random.Range(0, list.Count + 1);
        print(rnd);
    }

    //the cat needs to go to a random hide camera

}
