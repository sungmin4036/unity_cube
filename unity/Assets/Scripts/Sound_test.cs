using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_test : MonoBehaviour
{
    
    private AudioSource theAudio;

    [SerializeField] private AudioClip[] clip;
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        
    }

    public void PlaySE()
    {
        int _temp = 0;
        theAudio.clip = clip[_temp];
        theAudio.Play();
    }


}
