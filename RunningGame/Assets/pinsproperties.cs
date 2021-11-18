using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public enum state { active, deactive, current }

public class pinsproperties : MonoBehaviour { 

    public bool Hascompleted;
    public int level;
    public Image pinimage;
    public Color colour;
    public state mystate=state.deactive;
    public Animator anim;
    [SerializeField] private Image india;
    [SerializeField]private Transform pinpointloc;
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        pinimage = gameObject.GetComponent<Image>();
        pinpointloc = gameObject.GetComponent<Transform>();
        india = GameObject.Find("India").GetComponent<Image>();
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
        if (mystate == state.deactive)
        {
            anim.Play("default");
        }


    }

    public void clickme()
    {
        Debug.Log("click");
        india.rectTransform.pivot = pinpointloc.position;
      
    }
    public void onclickcity()
    {

    }


}
