using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Level : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Animator shopanimatior;


    public void shop()
    {
        anim.SetBool("Stop",false);
    }
    public void shopback(GameObject OBJ)
    {
        anim.SetBool("Stop",true);
        shopanimatior.SetBool("Close",true);
        StartCoroutine(BACKaNIMTION(OBJ));
        
    }
    IEnumerator BACKaNIMTION(GameObject OBJ)
    {
        yield return new WaitForSeconds(.75f);
        OBJ.SetActive(false);
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
