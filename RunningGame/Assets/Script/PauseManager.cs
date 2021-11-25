using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {   
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
     }
    public void Gameplay()
    {
        LoadingScreen.Loading.BackButton((int)ScenceConect.StartMenu);
        Time.timeScale = 1f;
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
