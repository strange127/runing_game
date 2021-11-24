using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundSetting : MonoBehaviour
{
    public Image soundOn;
    public Image soundOff;
    public float volumeVlaue;
    public bool ismuted;
    public AudioClip[] BackGroundSound;
    public AudioSource backgroundAudio;

    private void Awake()
    {
        backgroundAudio.clip = BackGroundSound[Random.Range(0, BackGroundSound.Length)];
        backgroundAudio.Play();
    }
    private void Start()
    {
       
        if(!PlayerPrefs.HasKey("Muted"))
        {
            PlayerPrefs.SetInt("Muted", 1);
            Load();
        }
        else
        {
            Load();
        }
        
        volumeVlaue = PlayerPrefs.GetFloat("SoundVolume");
        
        UpdateButtonIcon();
        AudioListener.pause = ismuted;
        
    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G))
            SoundButton();
    }
    public void ChangeSoundVoulume()
    {
        AudioListener.volume = volumeVlaue;
        PlayerPrefs.SetFloat("SoundVolume",volumeVlaue);
     
    }
   public void SoundButton()
    {
        ismuted = !ismuted;
        AudioListener.pause = !AudioListener.pause;
        Save();
        UpdateButtonIcon();
        
    }
    void UpdateButtonIcon()
    {
        //if (ismuted)
        //{
        //    soundOn.enabled = false;
        //    soundOff.enabled = true; 
        //}else
        //{
        //    soundOn.enabled = true;
        //    soundOff.enabled = false;
        //}
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Muted",ismuted ? 1:0);
    }
    public void Load()
    {
        ismuted = PlayerPrefs.GetInt("Muted") == 1;
    }
}
