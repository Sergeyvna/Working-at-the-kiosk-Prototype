using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundAudio : MonoBehaviour
{
    private static BackgroundAudio instance;
    private AudioSource audioSource;


    private void Awake()
    {
        
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }   
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }            
    }


    public void ChangeBackgroundMusic(AudioClip audio)
    {
        audioSource = GetComponent<AudioSource>();

        if(audio == null)
            return;

        if(audioSource.clip != audio)
        {
            audioSource.Stop();
            audioSource.clip = audio;
            audioSource.Play();

        }

    }


}
