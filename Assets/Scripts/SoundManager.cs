using Lofelt.NiceVibrations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public HapticSource hapticSource;
    public AudioSource audioSource;
    public AudioClip Drop;
    public AudioClip Take;
    public AudioClip OpenStation;
    public AudioClip CashCollect;
    public AudioClip ButtonClick;
    public AudioClip Sword;
    public AudioClip Hit;
    public AudioClip PickAxe;
    public AudioClip Axe;
    public AudioClip FootStep;
    public AudioClip Teleport;
    public AudioClip Gain;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip Money;
    public AudioClip Arrow;
    public AudioClip Fireball;
    public AudioClip armourWalk;
    public void PlayOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip); HapticFeedback();
    }
    public void PlayOneShot(AudioClip clip,float volume)
    {
        audioSource.PlayOneShot(clip,volume); HapticFeedback();
    }
    public void HapticFeedback()
    {
       hapticSource.Play();
    }
    public void HapticClick()
    {
      hapticSource.level = 0;
      AudioListener.volume = 1;
    }
    public void SoundClick()
    {
       AudioListener.volume = 0;
       hapticSource.level = 1;
    }
}
