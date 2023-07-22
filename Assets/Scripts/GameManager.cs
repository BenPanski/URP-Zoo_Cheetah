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
    [SerializeField] AudioClip PreCat;
    [SerializeField] AudioClip HideMusic;
    [SerializeField] AudioClip RunMusic;
    [SerializeField] AudioSource audioSource;


    bool GameStarted = false;
    private void Start()
    {
        if (PreCat)
        {
            swapPlayAudio(PreCat);
        }
    }


    public void swapPlayAudio(AudioClip clip) 
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    // Start is called before the first frame update
    public void CatWon()
    {
        if (!SomeoneWon)
        {
            CatFinishedRace = true;
            //  StartCoroutine(WaitUntilPlayerWon());
            catCoughtPlayer.SetActive(true);
            SomeoneWon = true;
            Invoke("RestartGame", 20);
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
            Invoke("RestartGame", 20);
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
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
            Invoke("SetCatActive", 5f);
            print("countdown started");
        }
    }

    public void SetRunMusic() 
    {
        if (RunMusic)
        {
            swapPlayAudio(RunMusic);
        }
    }

    public void SetCatActive() // called from start game 
    {

        if (HideMusic)
        {
            swapPlayAudio(HideMusic);
        }
        Cat.SetActive(true);
        StartingTimer.SetActive(false);
    }

}
