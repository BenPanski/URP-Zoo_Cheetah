using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTester : MonoBehaviour
{
    Animator myAnim;
    int count = 0;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            myAnim.SetInteger("AnimNum", count);
            count++;
            if (count == 4)
                count = 0;
        }
    }
}
