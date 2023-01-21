using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeReference] GameObject Cat;
    [SerializeReference] Animator countdownanim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (countdownanim.GetCurrentAnimatorStateInfo(0).IsName("end"))
        {
            Cat.SetActive(true);
            Destroy(gameObject);
        }

    }
}
