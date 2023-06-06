using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;
    public static AudioManager Instance;
    void Awake()
    {
        if(Instance == null)
        {
            Instance= this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume= s.volume;
            s.source.pitch= s.pitch;
            s.source.loop= s.loop;
            s.source.outputAudioMixerGroup = s.output;

        } 
    }

    public void Play(string name)
    {
       
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound of " + name + " not found!!");
            return;
        }
        s.source.Play();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound of " + name + " not found!!");
            return true;
        }
        return s.source.isPlaying;
    }

}
