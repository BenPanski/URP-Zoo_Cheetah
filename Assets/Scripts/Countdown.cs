using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeReference] GameObject Cat;
    [SerializeReference] Animator countdownanim;
    bool GameStarted = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!Cat)
        {
            Cat = FindObjectOfType<Cheetah>().gameObject;
        }
    }


  
    private void Update()
    {
        if (GameStarted)
        {
            Invoke("SetCatActive", 5f);
            print("countdown started");
            GameStarted = false;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameStarted = true;
        }
      
    }
    // Update is called once per frame


    public void SetCatActive()
    {
        Cat.SetActive(true);
        Destroy(gameObject);
    }
}
