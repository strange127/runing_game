using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class Level : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Image[] Audiosetup;
    private SoundSetting sound;
    // [SerializeField] Image[] buttons;

    private void Awake()
    {
        if (GameObject.Find("GameManager").GetComponent<SoundSetting>().ismuted == true)
        { Audiosetup[0].enabled = true; Audiosetup[1].enabled = false; }
        if (GameObject.Find("GameManager").GetComponent<SoundSetting>().ismuted == false)
        { Audiosetup[1].enabled = true; Audiosetup[0].enabled = false; }
            switch (GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue)
        {
            case 0.5f: Audiosetup[2].enabled = true;
                break;
            case 2.5f:
                Audiosetup[3].enabled = true;
                break;
            case 5f:
                Audiosetup[4].enabled = true;
                break;
            case 7.5f:
                Audiosetup[5].enabled = true;
                break;
            case 10f:
                Audiosetup[6].enabled = true;
                break;
        }

    }
    public void shop()
    {
        anim.SetBool("Stop",false);
    }

    public void shopback()
    {
        anim.SetBool("Stop",true);

    }
    public void volume(int i)
        
    {
        switch (i)
        {
            case 0:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = .5f;
                break;
            case 1:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 2.5f;
                break;
            case 2:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 5f;
                break;
            case 3:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 7.5f;
                break;
            case 4:
                GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue = 10f;
                break;
        }

        GameObject.Find("GameManager").GetComponent<SoundSetting>().ChangeSoundVoulume();
    }
    
   
    public void volumemute()
    {
        GameObject.Find("GameManager").GetComponent<SoundSetting>().SoundButton();

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
