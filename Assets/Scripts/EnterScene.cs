using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterScene : MonoBehaviour
{
    [SerializeField] 
    private bool isEntering;
    [SerializeField]
    private int sceneIndx;
    [SerializeField]
    private AudioClip audioToPlay;

    private BackgroundAudio backgroundAudio;
    
    private LevelLoader levelLoader;


    private void Start() {
        levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        backgroundAudio = FindObjectOfType<BackgroundAudio>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GameObject player = other.gameObject;

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            levelLoader.SkipScene();
            Player.instance.sceneIndx = sceneIndx;
            backgroundAudio.ChangeBackgroundMusic(audioToPlay);
        }
        else if(isEntering)
        {
            levelLoader.LoadNextGameScene();
            Player.instance.sceneIndx = sceneIndx;
            backgroundAudio.ChangeBackgroundMusic(audioToPlay);
        }  
        else
        {
            levelLoader.LoadPreviousGameScene();
            Player.instance.sceneIndx = sceneIndx;
            backgroundAudio.ChangeBackgroundMusic(audioToPlay);
        }
            
    }


}
