using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    Slider slider
    {
        get { return GetComponent<Slider>(); }
    }

    public AudioMixer mixer;
    public string volumeName;
    public string prefName;

    public void UpdateValueOnChange(float value)
    {
        if (mixer)
        {
            mixer.SetFloat(volumeName, Mathf.Log10(value) * 20f);
            Debug.Log("Change the value of " + mixer.name);
        }

        PlayerPrefs.SetFloat(prefName,value);
    }
}
