using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public LevelCreationScriptable[] selectedLevel;
    public GameObject loadingScreen;
    public string loadScenence;
    private void Awake()
    {
        //SceneManager.LoadSceneAsync((int)0, LoadSceneMode.Additive);
    }
    AsyncOperation async;
    public void LoadingScence(int level)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync((int)1, LoadSceneMode.Additive);
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
                break;
            }
        }
        async = SceneManager.UnloadSceneAsync((int)0);
        while (!async.isDone)
        {
            yield return null;
        }
        loadingScreen.SetActive(false);

    }
}
