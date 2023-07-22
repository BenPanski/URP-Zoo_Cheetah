using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceNoneLoop;
    [SerializeField] AudioSource audioSourceLoop;
    [SerializeField] AudioClip BeforeCat;//sound 1
    [SerializeField] AudioClip TimerSound;// sound 2
    [SerializeField] AudioClip CatHunt;//sound3
    [SerializeField] AudioClip CatFound;// sound 4
    [SerializeField] AudioClip CatWasntFound;// sound 5
    [SerializeField] AudioClip RunScreen;//sound 6
    [SerializeField] AudioClip CatWon;// sound 7
    [SerializeField] AudioClip PlayersWon;// sound 8

    public void swapPlayAudio(AudioClip clip)
    {
        audioSourceNoneLoop.Stop();
        audioSourceNoneLoop.clip = clip;
        audioSourceNoneLoop.Play();
    }
    public void swapPlayAudioLoop(AudioClip clip)
    {
        audioSourceLoop.Stop();
        audioSourceLoop.clip = clip;
        audioSourceLoop.Play();
    }
    public void TurnOffLoops()
    {
        audioSourceLoop.Stop();
    }
    
    
    public void PlayTimer()
    {
        swapPlayAudioLoop(TimerSound);
    } // sound 2
    public void PlayBeforeCat()
    {
        swapPlayAudioLoop(BeforeCat);
    } //sound 1
    public void PlayCatFound()
    {
        swapPlayAudio(CatFound);
    } // sound 4
    public void PlayCatWasntFound()
    {
        swapPlayAudio(CatWasntFound);
    }// sound 5
    public void PlayRunScreen()
    {
        swapPlayAudioLoop(RunScreen);
    } //sound 6
    public void PlayCatWon()
    {
        swapPlayAudio(CatWon);
        TurnOffLoops();
    } // sound 7
    public void PlayPlayersWon()
    {
        TurnOffLoops();
        swapPlayAudio(PlayersWon);
    } // sound 8
    public void PlayCatHunt()
    {
        swapPlayAudio(CatHunt);
    }//sound3


}
