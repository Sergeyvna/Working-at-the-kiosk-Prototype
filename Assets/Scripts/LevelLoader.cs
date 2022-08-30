using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField]
    private Animator fadeTransition;
    [SerializeField]
    private Animator fightTxt;
    [SerializeField]
    private float fadeTransitionTime = 1f;
    [SerializeField]
    private float txtTransitionTime = 1f;

    

    public void LoadNextGameScene()
    {
        StartCoroutine(LoadFade(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousGameScene()
    {
        StartCoroutine(LoadFade(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void LoadFightScene()
    {
        StartCoroutine(LoadFightTxt());
        StartCoroutine(LoadFade(5));
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadFade(4));
    }

    public void LoadGoingHomeScene()
    {
        StartCoroutine(LoadFade(3));
    }

    public void SkipScene()
    {
        StartCoroutine(LoadFade(SceneManager.GetActiveScene().buildIndex - 2));
    }

    IEnumerator LoadFightTxt()
    {
        fightTxt.SetTrigger("FightTxt");
        yield return new WaitForSeconds(txtTransitionTime);
    }

    IEnumerator LoadFade(int levelIndex)
    {
        fadeTransition.SetTrigger("Start");
        yield return new WaitForSeconds(fadeTransitionTime);
        SceneManager.LoadScene(levelIndex);
        yield return new WaitForSeconds(1f);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
