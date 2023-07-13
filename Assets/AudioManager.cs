using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;


    private void Awake()
    {
        if ( instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds )
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if ( s == null )
        {
            Debug.Log("the " + s.name + " sound is not found");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("the " + s.name + " sound is not found");
            return;
        }
        s.source.Stop();
    }
    public void TurnOffBGSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("the " + s.name + " sound is not found");
            return;
        }
        s.source.volume = 0f;
    }
    public void TurnOnBGSound(string name, float volumn)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("the " + s.name + " sound is not found");
            return;
        }
        s.source.volume = volumn;
    }
    public float GetVolumnOfSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("the " + s.name + " sound is not found");
            return 0f;
        }
        return s.source.volume;
    }
}
