using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Completedcontroller : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] GameObject medal1;
    [SerializeField] GameObject medal2;
    [SerializeField] GameObject medal3;
    [SerializeField] Text wintext;
    // Start is called before the first frame update
   
    void Start()
    {
   
    }
    private void OnEnable()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        switch (manager.Posstion)
        {
            case 1:
                medal3.SetActive(true);
                if(PlayerPrefs.GetInt("SaveGame") <=GameManager.instance.AutoLevelMaking.level)
                     PlayerPrefs.SetInt("SaveGame", PlayerPrefs.GetInt("SaveGame") + 1);
                wintext.text = "AWESOME";
                break;
            case 2:
                medal2.SetActive(true);
                if (PlayerPrefs.GetInt("SaveGame") <= GameManager.instance.AutoLevelMaking.level)
                    PlayerPrefs.SetInt("SaveGame", PlayerPrefs.GetInt("SaveGame") + 1);
                wintext.text = "GREAT";
                break;
            case 3:
                medal1.SetActive(true);
                if (PlayerPrefs.GetInt("SaveGame") <= GameManager.instance.AutoLevelMaking.level)
                    PlayerPrefs.SetInt("SaveGame", PlayerPrefs.GetInt("SaveGame") + 1);
                wintext.text = "GOOD";
                break;
            case 4:
                wintext.text = "NICE TRY";
                break;
            case 5:
                wintext.text = "BETTER LUCK";
                break;
        }
    }
    private void OnDisable()
    {
        medal1.SetActive(false);
        medal1.SetActive(false);
        medal1.SetActive(false);
        wintext.text = "";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
