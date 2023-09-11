using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject catCoughtPlayer;
    [SerializeField] GameObject PlayerWon;
    [SerializeField] Cheetah Cat;
    [SerializeField] GameObject FPSC;
    [SerializeField] GameObject StartingTimer;
    [SerializeField] GameObject End_Players_Won;
    [SerializeField] GameObject End_Players_Lost;
    [SerializeField] GameObject End_Players_Were_Wrong;
    [SerializeField] GameObject End_Out_Of_Power;


    [SerializeField] bool CatFinishedRace;
    [SerializeField] bool CatWasCought;
    [SerializeField] public bool PlayersWereWrongBool;
    [SerializeField] bool PlayerFinishedRace;
    [SerializeField] bool PlayersLost;
    [SerializeField] bool GameEnded;
    [SerializeField] public bool SomeoneWon;
    [SerializeField] bool FirstSensorTriggered;
    [SerializeField] SoundManager soundManager;
    [SerializeField] float RestartDelay = 10;
    [SerializeField] float UIDelay = 5;
    public float CatSpawnDelayMin = 2;
    public float CatSpawnDelayMax = 5;
    float GameCounter = 0;
    float ResetLoseCounter= 0;

    int playerLostTimes;
    bool PlayersWereWrongLastGame;
    bool GameStarted = false;

    // Screen mapping
    // game screen 6 = user screen 1
    public void CatWon()
    {
        if (!SomeoneWon & !PlayersWereWrongBool)
        {
            soundManager.PlayCatWon();
            CatFinishedRace = true;
            //  StartCoroutine(WaitUntilPlayerWon());
            SomeoneWon = true;
            if (!PlayerLost3Times())
            {
                print("bl bla");
                catCoughtPlayer.SetActive(true);
                //StartCoroutine(ShowEndUI(End_Players_Lost)); //hardcoded 5 seconds timer
                StartCoroutine(RestartGame()); // hardcoded 5 seconds timer
            }
            PlayerPrefs.SetInt("PlayersWereWrongBefore", 0);

        }

    }

    private void Update()
    {
        IdleChecker(); // checks if the game ran without play for 3 minutes -> reset player lost count


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

        if (PlayersWereWrongLastGame)
        {
            GameCounter += Time.deltaTime;
            print("counting" + GameCounter);
            if (GameCounter >= 4.5f)
            {
                PlayersWereWrongLastGame = false;
            }
            print("playerswerewronglastgame = " + PlayersWereWrongLastGame);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !PlayersWereWrongLastGame)
        {
            Cat.TryToCatchCat();
            IfNoCatPlayersWereWrong();
        }

    }

    private void IdleChecker()
    {
        if (!GameStarted)
        {
            ResetLoseCounter += Time.deltaTime;

            if (ResetLoseCounter > 180)
            {
                ResetLoseCounter = 0;
                PlayerPrefs.SetInt("PlayersWereWrongBefore", 1);
            }

        }
    }

    private void LoadCatSpawnTimerFromConfig()
    {
        string[] configLines = File.ReadAllLines(Application.streamingAssetsPath + "/cheetah.ini");
        if (configLines != null && configLines.Length > 0)
        {
            foreach (string line in configLines)
            {
                if (line != null && line.Length > 0 && line.Contains("="))
                {
                    string[] keyValue = line.Split('=');
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    if (key == "catSpawnDelayMin")
                    {
                        CatSpawnDelayMin = float.Parse(value);
                    }
                    else if (key == "catSpawnDelayMax")
                    {
                        CatSpawnDelayMax = float.Parse(value);
                    }
                }
            }
        }
    }

    public bool PlayerLost3Times()
    {
        UpdatePlayerLotTimes();

        if (playerLostTimes >= 3)
        {
            //open special UI
           // StartCoroutine(ShowEndUI(End_Out_Of_Power, 1));
            // Reset player lost count
            PlayerPrefs.SetInt("PlayerLostCount", 0);
            StartCoroutine(RestartGame());
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdatePlayerLotTimes()
    {
        playerLostTimes = PlayerPrefs.GetInt("PlayerLostCount");
        playerLostTimes++;
        PlayerPrefs.SetInt("PlayerLostCount", playerLostTimes);
    }

    public void IfNoCatPlayersWereWrong()
    {
        if (Cat.gameObject.activeSelf == false && GameStarted)
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
            Cat.TurnOffCatWasHereImages();
            Cat.gameObject.SetActive(false);
            PlayerWon.SetActive(true);
            SomeoneWon = true;
            PlayerPrefs.SetInt("PlayersWereWrongBefore", 0);
            //StartCoroutine(ShowEndUI(End_Players_Won)); //hardcoded 5 seconds timer
            StartCoroutine(RestartGame()); // hardcoded 5 seconds timer
        }

    }
    public void PlayersWereWrong()
    {
        if (!PlayersWereWrongBool && !SomeoneWon)
        {
           
            StartingTimer.SetActive(false);
            PlayersWereWrongBool = true;
            SomeoneWon = true;
            soundManager.PlayCatWasntFound();
            print("players are wrong");
            Cat.TurnOffCatWasHereImages();
            Cat.gameObject.SetActive(false);
            PlayerPrefs.SetInt("PlayersWereWrongBefore", 1);
            if (!PlayerLost3Times())  // if player didnt lose 3 times
            {
                // SET ACTIVE releveant ui
                StartCoroutine(ShowEndUI(End_Players_Were_Wrong, 0)); // hardcoded 1 seconds timer
                StartCoroutine(RestartGame(5)); // hardcoded 5 seconds timer
            }

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

    private void Awake()
    {
        playerLostTimes = PlayerPrefs.GetInt("PlayerLostCount", 0); // check how many times the player lost

        if (PlayerPrefs.GetInt("PlayersWereWrongBefore", 0) == 1) // if players were wrong last time
        {
            PlayersWereWrongLastGame = true;
            StartGame();
            PlayerPrefs.SetInt("PlayersWereWrongBefore", 0); // reset player preds for players were wrong
        }

        LoadCatSpawnTimerFromConfig();
    }
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
    public IEnumerator RestartGame()// 
    {
        yield return new WaitForSeconds(RestartDelay);
        SceneManager.LoadScene(0);
    }
    public IEnumerator RestartGame(int manualRestartDelay)// 
    {
        yield return new WaitForSeconds(manualRestartDelay);
        SceneManager.LoadScene(0);
    }
    public IEnumerator SetCatActive() // called from start game  // hardcoded 5 seconds
    {
        if (!PlayersWereWrongBool)
        {
            yield return new WaitForSeconds(5); // hardcoded 5 seconds for 5 seconds clock
                StartingTimer.SetActive(false);
            var RandCatDelay = Random.Range(CatSpawnDelayMin, CatSpawnDelayMax); // decide on cat spawn delay
            yield return new WaitForSeconds(RandCatDelay); // wait for the cat spawn delay to activate the cat
            if (!PlayersWereWrongBool)
            {
                Cat.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator ShowEndUI(GameObject EndUI)
    {
        yield return new WaitForSeconds(UIDelay);
        PlayerWon.SetActive(false);
        catCoughtPlayer.SetActive(false);

        EndUI.SetActive(true);
    }
    public IEnumerator ShowEndUI(GameObject EndUI, float Costumedelay)
    {
        yield return new WaitForSeconds(Costumedelay);
        PlayerWon.SetActive(false);
        catCoughtPlayer.SetActive(false);
        EndUI.SetActive(true);
    }
   
}
