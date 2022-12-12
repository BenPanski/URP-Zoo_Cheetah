using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catCoughtPlayer;
    [SerializeField] GameObject PlayerWon;
    [SerializeField] GameObject Cat;
    [SerializeField] bool CatFinishedRace;
    // Start is called before the first frame update
    public void CatWon()
    {
         CatFinishedRace = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (CatFinishedRace)
            {
                catCoughtPlayer.SetActive(true);
            }
            else
            {
                PlayerWon.SetActive(true);
            }
        }
    }
}
