using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Level : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Animator shopanimatior;
    [SerializeField] SoundSetting sound;
   // [SerializeField] Image[] buttons;


    public void shop()
    {
        anim.SetBool("Stop",false);
    }
    public void shopback()
    {
        anim.SetBool("Stop",true);
        //shopanimatior.SetBool("Close",true);
        //StartCoroutine(BACKaNIMTION(OBJ));
        
    }
    public void volume(int i)
        
    {
        switch (i)
        {
            case 0:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 8f;
                break;
            case 1:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 11f;
                break;
            case 2:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 14f;
                break;
            case 3:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 17f;
                break;
            case 4:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 20f;
                break;
        }

        
    }
    
   
    public void volumemute(bool value)
    {
        GameObject.Find("GameManager").GetComponent<SoundSetting>().ismuted = value;

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
