using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Level : MonoBehaviour
{
    [SerializeField] Animator anim;



    public void shop()
    {
        anim.SetBool("Stop",false);
    }
    public void shopback()
    {
        anim.SetBool("Stop",true);
        
    }
    public void ModesMenu() {
        SceneManager.LoadScene(0);
    }

    public void LoadStartMenu() {
        SceneManager.LoadScene("Modes");
    }

 public void LoadSettingsMenu() {
     SceneManager.LoadScene("Settings");
 }

 public void QuitGame() {
     Application.Quit();
 }

 public void LoadLast() {
     SceneManager.LoadScene("End Game");
 }

 public void LoadOnlineMenu() {
     SceneManager.LoadScene("Online mode");
 }

void Update() {
 if(Input.GetKeyDown(KeyCode.P)) {
     SceneManager.LoadScene("Pause Menu");
 }
}


 
}
