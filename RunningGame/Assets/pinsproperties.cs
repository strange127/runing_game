using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;
public enum state { active, deactive, current }
public enum region { north,south,west,east}
public enum map { worldmap,india }

public class pinsproperties : MonoBehaviour { 

    public bool Hascompleted;
    public int level;
    public Image pinimage;
    public Color colour;
    public state mystate=state.deactive;
    [SerializeField] private map curmap;
    public Animator anim;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image india;
    [SerializeField]private Transform pinpointloc;
    [SerializeField] private region regn;
    [SerializeField] LoadingScreen gamemanager;
    public bool big = false;
 private int pressed =0;
    private void Awake()
    {
        if (curmap == map.india)
            india = GameObject.Find("India").GetComponent<Image>();
        
        anim = gameObject.GetComponent<Animator>();
      //  pinimage = gameObject.GetComponent<Image>();
        pinpointloc = gameObject.GetComponent<Transform>();
        gamemanager = GameObject.Find("GameManager").GetComponent<LoadingScreen>();
       
       // colour = pinimage.color;
        
    }

    private void Update()
    {
        if (mystate == state.active)
        {
            gameObject.GetComponent<Button>().interactable = true;
            anim.SetBool("currentstate", false);
            anim.SetBool("activestate",true);
           // pinimage.color = new Color32(1, 1, 1, 1);
        }
        if(mystate == state.current)
        {
            gameObject.GetComponent<Button>().interactable = true;
            anim.SetBool("currentstate", true);
            
        }
        if (mystate == state.deactive)
        {
            gameObject.GetComponent<Button>().interactable = false;
            anim.Play("default");
        }


    }

    public void clickme()
    {
        Debug.Log("click" + level + "  "+pressed);
       
            if (regn == region.east)
            {
                india.rectTransform.pivot = new Vector2(0.71f, 0.66f);
                india.rectTransform.localScale = new Vector3(3f, 3f, 3f);
                pressed++;

            }
            if (regn == region.west)
            {
                india.rectTransform.pivot = new Vector2(0.34f, 0.62f);
                india.rectTransform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
                pressed++;
            }
            if (regn == region.south)
            {
                india.rectTransform.pivot = new Vector2(0.38f, 0.11f);
                india.rectTransform.localScale = new Vector3(2.3f, 2.4f, 2.9f);
                pressed++;
            }
            if (regn == region.north)
            {
                india.rectTransform.pivot = new Vector2(0.36f, 1f);
                india.rectTransform.localScale = new Vector3(3.1f, 3.1f, 3.1f);
                pressed++;
            }

        if (pressed > 1)
        {
            gamemanager.LoadingScence(level);
            pressed = 0;
        }
        
        }
   


}
