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
    AsyncOperation async;
    private void Awake()
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)ScenceConect.StartMenu, LoadSceneMode.Additive);
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
        PlayGame.onClick.AddListener(() => LoadingScence(1));
    }
    public void Loaded()
    {
        async = SceneManager.LoadSceneAsync((int)ScenceConect.LevelSelction, LoadSceneMode.Additive);
        async = SceneManager.UnloadSceneAsync((int)ScenceConect.StartMenu);
    }
    public void LoadingScence(int level)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)ScenceConect.Game, LoadSceneMode.Additive);
        async = SceneManager.UnloadSceneAsync((int)ScenceConect.StartMenu);
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
     

        loadingScreen.SetActive(false);

    }
}
