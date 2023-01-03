using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catCoughtPlayer;
    [SerializeField] GameObject PlayerWon;
    [SerializeField] GameObject Cat;
    [SerializeField] bool CatFinishedRace;
    [SerializeField] bool PlayerFinishedRace;
    [SerializeField] bool PlayersLost;
    [SerializeField] bool GameEnded;
    [SerializeField] bool SomeoneWon;
    // Start is called before the first frame update
    public void CatWon()
    {
        CatFinishedRace = true;
        StartCoroutine(WaitUntilPlayerWon());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("players reached finish line");
            PlayerFinishedRace = true;
            StartCoroutine(WaitUntilCatWon());
        }

        if (CatFinishedRace && PlayerFinishedRace)
        {
            GameEnded = true;
        }


    }

    private IEnumerator WaitUntilPlayerWon()
    {
        if (!SomeoneWon)
        {
            SomeoneWon = true;
            yield return new WaitUntil(() => GameEnded);
            catCoughtPlayer.SetActive(true);
        }
    }

    private IEnumerator WaitUntilCatWon()
    {
        if (!SomeoneWon)
        {
            SomeoneWon = true;
            yield return new WaitUntil(() => GameEnded);
            PlayerWon.SetActive(true);
        }
    }
}
