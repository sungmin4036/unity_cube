using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;
    public AudioSource btnsource;
    public void SetMusicVolum(float volume) {
        musicsource.volume = volume;
    }

    public void SetButtonVolume(float volume) 
    {
        btnsource.volume = volume;
    }

    public void OnSfx() 
    {
        btnsource.Play();
    }

}
