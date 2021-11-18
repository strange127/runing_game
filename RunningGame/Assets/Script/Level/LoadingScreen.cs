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
        startingScence.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Single));
     //   startingScence.Add(SceneManager.UnloadSceneAsync(0));
        StartCoroutine(StartMenu());
    }
    IEnumerator StartMenu()
    {
        for (int i = 0; i < async.Count; i++)
        {
            while (!startingScence[i].isDone)
            {
                yield return null;
            }

        }

        loadingScreen.SetActive(false);
        PlayGame = GameObject.Find("Canvas/MainMenu/Play").GetComponent<Button>();
        PlayGame.onClick.AddListener(() => LoadingScence(levelLoad));
    }
    List<AsyncOperation> async;
    public List<AsyncOperation> startingScence;
    public void LoadingScence(int level)
    {
        loadingScreen.SetActive(true);
      
        async.Add(SceneManager.UnloadSceneAsync((int)1));
        async.Add(SceneManager.LoadSceneAsync((int)2, LoadSceneMode.Additive));
        StartCoroutine(GetScenceLoadProgress(level));
       
    }
    public IEnumerator GetScenceLoadProgress(int lvel)
    {
        for (int i = 0; i < selectedLevel.Length; i++)
        {
            if(selectedLevel[i].level == lvel)
            {
                GameManager.instance.AutoLevelMaking = selectedLevel[i];
                GameManager.instance.LevelCreate();
                break;
            }
        }
        for (int i = 0; i < async.Count; i++)
        {
            while (!async[i].isDone)
            {
                yield return null;
            }
        }

     

        loadingScreen.SetActive(false);

    }
}
