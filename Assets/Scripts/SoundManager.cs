using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceNoneLoop;
    [SerializeField] AudioSource audioSourceLoop;
    [SerializeField] AudioSource audioSourceLoopMusic;
    [SerializeField] AudioSource CatRunning;
    
    
    [SerializeField] AudioClip CatMusic;//sound 1
    [SerializeField] AudioClip TimerSound;// sound 2
    [SerializeField] AudioClip CatHunt;//sound3
    [SerializeField] AudioClip CatFound;// sound 4
    [SerializeField] AudioClip CatWasntFound;// sound 5
    [SerializeField] AudioClip RunScreen;//sound 6
    [SerializeField] AudioClip CatWon;// sound 7
    [SerializeField] AudioClip PlayersWon;// sound 8

    public void swapPlay(AudioClip clip)
    {
        audioSourceNoneLoop.Stop();
        audioSourceNoneLoop.clip = clip;
        audioSourceNoneLoop.Play();
    }
    public void swapPlayLoop(AudioClip clip)
    {
        audioSourceLoop.Stop();
        audioSourceLoop.clip = clip;
        audioSourceLoop.Play();
    }

    public void swapPlayLoopMusic(AudioClip clip)
    {
        audioSourceLoopMusic.Stop();
        audioSourceLoopMusic.clip = clip;
        audioSourceLoopMusic.Play();
    }
    public void TurnOffLoops()
    {
        audioSourceLoop.Stop();
        audioSourceLoopMusic.Stop();
        CatRunning.Stop();
    }

    public void PlayBeforeCat() //sound 1
    {
        //   swapPlayLoop(BeforeCat);
    }
    public void AfterTimer()// sound 2
    {
        swapPlayLoop(TimerSound);
        swapPlayLoopMusic(CatMusic);
    }
    public void PlayCatHunt()//sound3
    {
        swapPlay(CatHunt);
    }
    public void PlayCatFound()
    {
        swapPlay(CatFound);
    } // sound 4
    public void PlayCatWasntFound()// sound 5
    {
        TurnOffLoops();
        swapPlay(CatWasntFound);
    }
    public void PlayRunScreen() //sound 6
    {
        CatRunning.Stop();
        CatRunning.clip =RunScreen;
        CatRunning.Play();
    }
    public void PlayCatWon()// sound 7
    {
        swapPlay(CatWon);
        TurnOffLoops();
    } 
    public void PlayPlayersWon()// sound 8
    {
        TurnOffLoops();
        swapPlay(PlayersWon);
    } 
   


}
