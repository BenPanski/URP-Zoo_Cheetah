using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffInputsScreen : MonoBehaviour
{
    [SerializeField] GameObject InputText;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            InputText.SetActive(!InputText.activeSelf);
        }
    }
}
