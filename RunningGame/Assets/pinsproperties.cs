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
     
       if(pinpointloc.position.x >= 0 && pinpointloc.position.y>=0)
        {
            india.rectTransform.pivot = new Vector2(0.71f, 0.66f);
            india.rectTransform.localScale = new Vector3(3.4f, 3.4f, 3.4f);
        }
       if (pinpointloc.position.x < 0 && pinpointloc.position.x > -96 && pinpointloc.position.y > 0 && pinpointloc.position.y < 185.9)
        {
            india.rectTransform.pivot = new Vector2(0.34f, 0.62f);
            india.rectTransform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        }
        if (pinpointloc.position.x < 0  && pinpointloc.position.y < 0)
        {
            india.rectTransform.pivot = new Vector2(0.38f, 0.11f);
            india.rectTransform.localScale = new Vector3(2.6f,2.4f,2.9f);
        }
        if (pinpointloc.position.x < -96 && pinpointloc.position.y > 185.9)
        {
            india.rectTransform.pivot = new Vector2(0.36f, 1f);
            india.rectTransform.localScale = new Vector3(3.1f, 3.1f, 3.1f);
        }
        }
    public void onclickcity()
    {

    }


}
