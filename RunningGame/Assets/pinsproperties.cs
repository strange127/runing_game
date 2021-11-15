using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public enum state { active, deactive, current }

public class pinsproperties : MonoBehaviour { 

    public bool Hascompleted;
    public Image pinimage;
    public Color colour;
    public state mystate=state.deactive;
    public Animator anim;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        pinimage = gameObject.GetComponent<Image>();
        colour = pinimage.color;
        
    }

    private void Update()
    {
        if (mystate == state.active)
        {
            anim.SetBool("currentstate", false);
            anim.SetBool("activestate",true);
            pinimage.color = new Color32(1, 1, 1, 1);
        }
        if(mystate == state.current)
        {
            anim.SetBool("currentstate", true);
        }
       
        
    }


}
