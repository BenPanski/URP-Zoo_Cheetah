using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catCoughtPlayer;
    [SerializeField] GameObject PlayerWon;
    [SerializeField] GameObject Cat;
    // Start is called before the first frame update
    public void CatWon()
    {
        catCoughtPlayer.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Cat.SetActive(false);
            PlayerWon.SetActive(true);
            
        }
    }
}
