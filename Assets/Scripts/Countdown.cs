using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeReference] GameObject Cat;
    [SerializeReference] Animator countdownanim;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!Cat)
        {
            Cat = FindObjectOfType<Cheetah>().gameObject;
        }
    }

    void Start()
    {
        print("countdown started");
        Invoke("SetCatActive", 5f);
    }

  

    public void SetCatActive()
    {
        Cat.SetActive(true);
        Destroy(gameObject);
    }
}
