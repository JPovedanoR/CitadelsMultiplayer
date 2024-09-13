using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour{
    [SerializeField] AudioMixer audioMixer;
    public Slider slider;
    
    public void SetVolume(float volume){
        audioMixer.SetFloat("musicvolume",slider.value);

    }
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen=isFullscreen;

    }
}
