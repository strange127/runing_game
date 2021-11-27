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

    void Start()
    {
        if (GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue >= 0.5)
        {
            Audiosetup[2].enabled = true;
            if (GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue >= 2.5)
            {
                Audiosetup[2].enabled = true;
                if (GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue >= 5)
                {
                    Audiosetup[4].enabled = true;
                    if (GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue >= 7.5)
                    {
                        Audiosetup[5].enabled = true;
                        if (GameObject.Find("GameManager").GetComponent<SoundSetting>().volumeVlaue == 10)
                        {
                            Audiosetup[6].enabled = true;
                        }
                    }
                }
            }
        }

        if (GameObject.Find("GameManager").GetComponent<SoundSetting>().ismuted == true)
        { Audiosetup[0].enabled = false; Audiosetup[1].enabled = true; }
        if (GameObject.Find("GameManager").GetComponent<SoundSetting>().ismuted == false)
        { Audiosetup[1].enabled = false; Audiosetup[0].enabled = true; }

        

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


 public void QuitGame() {
     Application.Quit();
 }

}




