using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource mainPanel;
    public AudioSource title;
    public AudioSource button;
    public float panelPlayTime;
    public float titlePlayTime;
    public float buttonPlayTime;

    public Slider mainVolume;
    public Slider sfxVolume;
    public Slider zombieVolume;
    public Slider sensitivity;


    private void OnEnable()
    {
        StartCoroutine(PlayAudio());
        if(PlayerPrefs.HasKey("MainVolume"))
        {
            mainVolume.value = PlayerPrefs.GetFloat("MainVolume");
            mainVolume.GetComponent<AudioSlider>().UpdateValueOnChange(mainVolume.value);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume");
            sfxVolume.GetComponent<AudioSlider>().UpdateValueOnChange(sfxVolume.value);

        }
        if (PlayerPrefs.HasKey("ZombieVolume"))
        {
            zombieVolume.value = PlayerPrefs.GetFloat("ZombieVolume");
            zombieVolume.GetComponent<AudioSlider>().UpdateValueOnChange(zombieVolume.value);

        }
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            sensitivity.value = PlayerPrefs.GetFloat("Sensitivity");
            sensitivity.GetComponent<AudioSlider>().UpdateValueOnChange(sensitivity.value);

        }

    }
    
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator PlayAudio()
    {
        yield   return new WaitForSeconds(panelPlayTime);
        mainPanel.Play();
        yield return new WaitForSeconds(titlePlayTime);
        title.Play();
    }
}
