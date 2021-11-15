using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{  
    [SerializeField]
    pinsproperties[] pins;
    // Start is called before the first frame update
    private void Awake()
    {
        for(int i = 0; i < pins.Length; i++)
        {
            if (i == 0)
                pins[i].mystate = state.current;
            if (pins[i - 1].Hascompleted == true)
            {
                pins[i - 1].mystate = state.active;
                 pins[i].mystate = state.current;
            }
           

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
