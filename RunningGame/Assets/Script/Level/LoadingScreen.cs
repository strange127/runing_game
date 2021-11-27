using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Loading;
    public LevelCreationScriptable[] selectedLevel;
    public GameObject camera;
    private Button PlayGame;

    public AsyncOperation async;

    public int curentsecne;
    public int previousscen;
    private void Awake()
    {
   
        Loading = this;
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
   //     loadingScreen.SetActive(false);
        PlayGame = GameObject.Find("Canvas/SelectMode/LevelMode").GetComponent<Button>();
        PlayGame.gameObject.transform.parent.gameObject.SetActive(false);
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
        buton.onClick.AddListener(() => BackButton((int)ScenceConect.StartMenu));
    }
    public void LoadingScence(int level)
    {

  //      loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)ScenceConect.Game, LoadSceneMode.Additive);
        if (level > 0)
        {
            curentsecne = (int)ScenceConect.Game;
            previousscen = (int)ScenceConect.LevelSelction;
            async = SceneManager.UnloadSceneAsync((int)ScenceConect.LevelSelction);
        }
        else
        {
            curentsecne = (int)ScenceConect.Game;
            previousscen = (int)ScenceConect.StartMenu;
            async = SceneManager.UnloadSceneAsync((int)ScenceConect.StartMenu);
        }
        StartCoroutine(GetScenceLoadProgress(level));
       
    }
    public void BackButton(int level)
    {
        camera.transform.parent =GameObject.Find("CameraParent").transform;
        GameManager.instance.Road.Clear();
        async = SceneManager.UnloadSceneAsync(curentsecne);
        async = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
        curentsecne = level;
        if(level == (int)ScenceConect.StartMenu)
             StartCoroutine(StartMenu());
        else if(level== (int)ScenceConect.LevelSelction)
            StartCoroutine(LoadLevelSelction());
    }
    
    public IEnumerator GetScenceLoadProgress(int lvel)
    {
        while (!async.isDone)
        {
            yield return null;
        }
        GameManager.instance.SpawnHolder = GameObject.Find("SpawnHolder").transform;
        for (int i = 0; i < selectedLevel.Length; i++)
        {
            if(selectedLevel[i].level == lvel)
            {
                GameManager.instance.AutoLevelMaking = selectedLevel[i];
                GameManager.instance.LevelCreate();
                break;
            }
        }
     

      //  loadingScreen.SetActive(false);

    }
}
