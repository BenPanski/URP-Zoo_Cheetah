using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catCoughtPlayer;
    [SerializeField] GameObject PlayerWon;
    [SerializeField] GameObject Cat;
    [SerializeField] bool CatFinishedRace;
    [SerializeField] bool PlayersLost;
    [SerializeField] bool GameEnded;
    // Start is called before the first frame update
    public void CatWon()
    {
        CatFinishedRace = true;
    }

    private void Update()
    {
        if (!GameEnded)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (CatFinishedRace)
                {
                    PlayersLost = true;
                }
            }

            if (CatFinishedRace)
            {
                if (PlayersLost)
                {
                    catCoughtPlayer.SetActive(true);
                }
                else
                {
                    PlayerWon.SetActive(true);
                }
                GameEnded = true;
            }
        }
    }
}
