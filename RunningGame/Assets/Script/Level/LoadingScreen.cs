using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public LevelCreationScriptable[] selectedLevel;
    public GameObject loadingScreen;
    private Button PlayGame;
    public int levelLoad;
    private void Awake()
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)1, LoadSceneMode.Additive);
        StartCoroutine(StartMenu());
    }
    IEnumerator StartMenu()
    {
        while (!async.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);
        PlayGame = GameObject.Find("Canvas/MainMenu/Play").GetComponent<Button>();
        PlayGame.onClick.AddListener(() => LoadingScence(levelLoad));
    }
    AsyncOperation async;
    public void LoadingScence(int level)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.UnloadSceneAsync((int)1);
        async = SceneManager.LoadSceneAsync((int)2, LoadSceneMode.Additive);
        StartCoroutine(GetScenceLoadProgress(level));
       
    }
    public IEnumerator GetScenceLoadProgress(int lvel)
    {
        while (!async.isDone)
        {
            yield return null;
        }

        for (int i = 0; i < selectedLevel.Length; i++)
        {
            if(selectedLevel[i].level == lvel)
            {
                GameManager.instance.AutoLevelMaking = selectedLevel[i];
                GameManager.instance.LevelCreate();
                break;
            }
        }
     
        while (!async.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);

    }
}
