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
    AsyncOperation async;
 
    // Start is called before the first frame update
    private void Awake()
    {
        for(int i = 0; i < pins.Length; i++)
        {    if (i == 0)
                pins[i].mystate = state.current;
           else if (i>0 && pins[i - 1].Hascompleted == true)
            {
                pins[i - 1].mystate = state.active;
                 pins[i].mystate = state.current;
            }

            pins[i].level = i+1;
        }
        
    }

  

    void Start()
    {
        
    }
   public void back()
    {
        if (india.localScale.x < 1.5)
        {
            panelon.SetActive(true);
            paneloff.SetActive(false);
        }
        if (india.localScale.x > 1.5)
          india.localScale = new Vector3(1.3f, 1.3f, 1.3f);

    }
    public void worldback()
    {
        async = SceneManager.UnloadSceneAsync((int)2);
        async = SceneManager.LoadSceneAsync((int)1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
