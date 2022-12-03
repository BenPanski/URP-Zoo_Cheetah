using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVid : MonoBehaviour
{
    public void Awake()
    {
        Time.timeScale = 0;
    }
    public void Start()
    {
        StartCoroutine(DestroyAfter6sec());
    }

    public void DestroyThis() 
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    IEnumerator DestroyAfter6sec()
    {
        {
            print("WaitAndPrint " + Time.time);
            yield return new WaitForSecondsRealtime(6);
            Time.timeScale = 1;

            Destroy(this.gameObject);
            print("finished");
            //do something after 5 seconds 

        }
    }
}


