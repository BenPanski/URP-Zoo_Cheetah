using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
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
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayTimer()
    {
        swapPlayAudio(TimerSound);
    } // sound 2
    public void PlayBeforeCat()
    {
        swapPlayAudio(BeforeCat);
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
        swapPlayAudio(RunScreen);
    } //sound 6
    public void PlayCatWon()
    {
        swapPlayAudio(CatWon);
    } // sound 7
    public void PlayPlayersWon()
    {
        swapPlayAudio(PlayersWon);
    } // sound 8
    public void PlayCatHunt()
    {
        swapPlayAudio(CatHunt);
    }//sound3
}
