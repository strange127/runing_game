using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Loading;
    public LevelCreationScriptable[] selectedLevel;
    public GameObject loadingScreen;
    private Button PlayGame;

    public AsyncOperation async;

    public int curentsecne;
    public int previousscen;
    private void Awake()
    {
   
        Loading = this;
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)ScenceConect.StartMenu, LoadSceneMode.Additive);
        previousscen = (int)ScenceConect.StartMenu - 1;
        curentsecne = (int)ScenceConect.StartMenu;
        StartCoroutine(StartMenu());
    }
    public IEnumerator StartMenu()
    {
        while (!async.isDone)
        {
            yield return null;
        }
       // async = SceneManager.UnloadSceneAsync((int)ScenceConect.LevelSelction);
        loadingScreen.SetActive(false);
        PlayGame = GameObject.Find("Canvas/MainMenu/Play").GetComponent<Button>();
        PlayGame.onClick.AddListener(() => Loaded());
    }
    public void Loaded()
    {
        async = SceneManager.UnloadSceneAsync((int)1);
        async = SceneManager.LoadSceneAsync((int)ScenceConect.LevelSelction, LoadSceneMode.Additive);
        
        curentsecne = (int)ScenceConect.LevelSelction;
        previousscen = (int)ScenceConect.LevelSelction - 1;
        StartCoroutine(LoadLevelSelction());
    }
    IEnumerator LoadLevelSelction()
    {
        while (!async.isDone)
        {
            yield return null;
        }
        Button buton = GameObject.Find("LevelSelctionCanvas/Panel-worldmap/worldmap/Backbutton").GetComponent<Button>();
        buton.onClick.AddListener(() => BackButton());
    }
    public void LoadingScence(int level)
    {
        if (GameManager.instance.levelLoad < level)
        {
            PlayerPrefs.SetInt("SaveGame", level);
        }
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)3, LoadSceneMode.Additive);
        curentsecne = (int)3;
        previousscen = 2;
        async = SceneManager.UnloadSceneAsync((int)2);
        StartCoroutine(GetScenceLoadProgress(level));
       
    }
    public void BackButton()
    {
        async = SceneManager.UnloadSceneAsync(curentsecne);
        async = SceneManager.LoadSceneAsync(previousscen, LoadSceneMode.Additive);
        StartCoroutine(StartMenu());
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
