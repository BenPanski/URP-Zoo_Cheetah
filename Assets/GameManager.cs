using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catCoughtPlayer;
    [SerializeField] GameObject PlayerWon;
    [SerializeField] GameObject Cat;
    [SerializeField] GameObject FPSC;
    [SerializeField] GameObject StartingTimer;
    [SerializeField] bool CatFinishedRace;
    [SerializeField] bool PlayerFinishedRace;
    [SerializeField] bool PlayersLost;
    [SerializeField] bool GameEnded;
    [SerializeField] bool SomeoneWon;

    bool GameStarted = false;


    // Start is called before the first frame update
    public void CatWon()
    {
        if (!SomeoneWon)
        {
            CatFinishedRace = true;
            //  StartCoroutine(WaitUntilPlayerWon());
            catCoughtPlayer.SetActive(true);
            SomeoneWon = true;
        }
      
    }

    private void Update()
    {

        StartGame();

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayersFinishedRace();
        }



        if (CatFinishedRace && PlayerFinishedRace)
        {
            GameEnded = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            FPSC.SetActive(!FPSC.activeSelf);
        }
    }

    public void PlayersFinishedRace()/// Itay - this method is called when the second sensor is triggered
    {
        if (!SomeoneWon)
        {
            print("players reached finish line");
            PlayerFinishedRace = true;
            // StartCoroutine(WaitUntilCatWon());
            Cat.SetActive(false);
            PlayerWon.SetActive(true);
            SomeoneWon = true;
        }

    }

    /*private IEnumerator WaitUntilPlayerWon()
    {
        if (!SomeoneWon)
        {
            SomeoneWon = true;
            yield return new WaitUntil(() => GameEnded);
            catCoughtPlayer.SetActive(true);
        }
    }*/

  /*  private IEnumerator WaitUntilCatWon()
    {
        if (!SomeoneWon)
        {
            SomeoneWon = true;
            yield return new WaitUntil(() => GameEnded);
            PlayerWon.SetActive(true);
        }
    }*/
  

    private void StartGame()
    {
        if (GameStarted)
        {
            StartingTimer.SetActive(true);
            Invoke("SetCatActive", 5f);
            print("countdown started");
            GameStarted = false;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameStarted = true;
        }
    }



    public void SetCatActive()
    {
        Cat.SetActive(true);
        StartingTimer.SetActive(false);
    }

}
