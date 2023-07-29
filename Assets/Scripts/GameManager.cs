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
    [SerializeField] GameObject End_Players_Won;
    [SerializeField] GameObject End_Players_Lost;
    [SerializeField] GameObject End_Players_Were_Wrong;
    [SerializeField] GameObject End_Out_Of_Power; // not implemented


    [SerializeField] bool CatFinishedRace;
    [SerializeField] bool CatWasCought;
    [SerializeField] bool PlayersWereWrongBool;
    [SerializeField] bool PlayerFinishedRace;
    [SerializeField] bool PlayersLost;
    [SerializeField] bool GameEnded;
    [SerializeField] bool SomeoneWon;
    [SerializeField] bool FirstSensorTriggered;
    [SerializeField] SoundManager soundManager;
    [SerializeField] float RestartDelay = 10;
    [SerializeField] float UIDelay = 5;



    bool GameStarted = false;
  



    // Start is called before the first frame update
    public void CatWon()
    {
        if (!SomeoneWon & !PlayersWereWrongBool)
        {
            soundManager.PlayCatWon();
            CatFinishedRace = true;
            //  StartCoroutine(WaitUntilPlayerWon());
            catCoughtPlayer.SetActive(true);
            SomeoneWon = true;
            StartCoroutine(ShowEndUI(End_Players_Lost)); //hardcoded 5 seconds timer
            StartCoroutine(RestartGame()); // hardcoded 5 seconds timer
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayersFinishedRace();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            IfNoCatPlayersWereWrong();
        }
    }

    public void IfNoCatPlayersWereWrong()
    {
        if (Cat.activeSelf == false && GameStarted)
        {
            PlayersWereWrong();
        }
      
    }

    public void PlayersFinishedRace()/// Itay - this method is called when the second sensor is triggered
    {
        if (!SomeoneWon && CatWasCought && !PlayersWereWrongBool&& GameStarted)
        {
            soundManager.PlayPlayersWon();
            print("players reached finish line");
            PlayerFinishedRace = true;
            // StartCoroutine(WaitUntilCatWon());
            Cat.SetActive(false);
            PlayerWon.SetActive(true);
            SomeoneWon = true;
            StartCoroutine(ShowEndUI(End_Players_Won)); //hardcoded 5 seconds timer
            StartCoroutine(RestartGame()); // hardcoded 5 seconds timer
        }

    }
    public void PlayersWereWrong()
    {
        if (!PlayersWereWrongBool && !SomeoneWon)
        {
            PlayersWereWrongBool = true;
            soundManager.PlayCatWasntFound();
            print("players are wrong");
            Cat.SetActive(false);
            // SET ACTIVE releveant ui
            StartCoroutine(ShowEndUI(End_Players_Were_Wrong)); // hardcoded 1 seconds timer
            StartCoroutine(RestartGame()); // hardcoded 5 seconds timer
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


    public void StartGame()
    {

        if (GameStarted == false)
        {
            GameStarted = true;
            StartingTimer.SetActive(true);
            soundManager.AfterTimer();
            StartCoroutine(SetCatActive()); // hardcoded 5 seconds
            //soundManager.PlayCatHunt();
            print("countdown started");
        }
    }

    public void UpdateManagerCatWasCought()
    {
        CatWasCought = true;
    }
    public IEnumerator RestartGame()// hardcoded 5 seconds
    {
        yield return new WaitForSeconds(RestartDelay);
        SceneManager.LoadScene(0);
    }
    public IEnumerator SetCatActive() // called from start game  // hardcoded 5 seconds
    {
        if (!PlayersWereWrongBool)
        {
            yield return new WaitForSeconds(5); // hardcoded 5 seconds

            Cat.SetActive(true);
            StartingTimer.SetActive(false);
        }

    }

    public IEnumerator ShowEndUI(GameObject EndUI)
    {
        yield return new WaitForSeconds(UIDelay);
        PlayerWon.SetActive(false);
        catCoughtPlayer.SetActive(false);

        EndUI.SetActive(true);
    }
}
