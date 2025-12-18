using UnityEngine.Audio;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public string themeMusic;
    private Sound currentMusic;
    
    void Awake() {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
            s.source.spatialBlend = 0f;
        }
    }

    void Start() {
        Play(themeMusic); //Play music on start
    }

    void Update() {
        if (currentMusic != null && currentMusic.source.isPlaying) {
            if (currentMusic.source.time >= currentMusic.endTime) {
                currentMusic.source.time = currentMusic.startTime;
            }
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.loop)
        {
            currentMusic = s;
        }
        else
        {
            s.source.time = s.startTime;
        }

        Debug.Log("Playing sound: " + name);
        s.source.Play();
    }

    public void PlayRanPitch(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        float ranPitch = UnityEngine.Random.Range(.95f, 1.05f);
        s.source.pitch = s.pitch * ranPitch;

        s.source.time = s.startTime;

        s.source.Play();
    }
}