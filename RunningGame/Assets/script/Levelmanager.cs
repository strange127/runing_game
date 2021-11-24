using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelmanager : MonoBehaviour
{  
    [SerializeField]
    pinsproperties[] pins;
    [SerializeField] Transform india;
    [SerializeField] GameObject paneloff;
    [SerializeField] GameObject panelon;
    public pinsproperties selectedpin;


    // Start is called before the first frame update
    private void OnEnable()
    {
        pins[0].mystate = state.current;
        for (int i = 0; i < pins.Length; i++)
        {    
            if (pins[i].level == PlayerPrefs.GetInt("SaveGame"))
             pins[i].mystate = state.current;
            if (pins[i].level < PlayerPrefs.GetInt("SaveGame"))
                pins[i].mystate = state.active;
            if (pins[i].level > PlayerPrefs.GetInt("SaveGame"))
                pins[i].mystate = state.deactive;
            
            pins[i].level = i+1;
            //if (i>0 && pins[i - 1].Hascompleted == true)
            //{
            //    pins[i - 1].mystate = state.active;
            //     pins[i].mystate = state.current;
            //}
           
        }
        
    }
 
    
   public void back()
    {
        if (india.localScale.x < 1.5)
        {
            panelon.SetActive(true);
            paneloff.SetActive(false);

        }
        if (india.localScale.x > 1.5)
        {
            selectedpin.big = false;
            selectedpin.pressed = 0;
            india.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            Debug.Log(selectedpin.big);
        }
        

    }

}
