using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public AudioSource musicSource;
    public AudioSource[] soundLoops;
    public AudioSource[] soundSources;
    private Queue<AudioSource> _queueSources;

    public List<AudioClip> sfxs;
    //
    // public AudioClip RandomSFX()
    // {
    //     return sfxs[Random.Range(0, sfxs.Count)];
    // }
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            _queueSources = new Queue<AudioSource>(soundSources);
        }
        OnInit();
    }
    public void OnInit()
    {
        ChangeSoundVolume();
        ChangeMusicVolume();
    }

    public void ChangeSoundVolume()
    {
        foreach (var sound in soundSources)
        {
            //sound.volume = UserData.FxSound;
            //sound.mute = !UserData.IsSoundOn;
            sound.mute = false;

        }
        foreach (var sound in soundLoops)
        {
            //sound.volume = UserData.FxSound;
            //sound.mute = !UserData.IsSoundOn;
            sound.mute = false;
        }
    }

    public void ChangeMusicVolume()
    {
        if (musicSource != null)
        {
            //musicSource.mute = !UserData.IsSoundOn;
            musicSource.mute = false;
            //musicSource.volume = UserData.Music;
        }
    }

    public void PlayShot(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            return;
        }
        var source = _queueSources.Dequeue();
        if (!source)
        {
            return;
        }
        //source.volume = volume;
        source.PlayOneShot(clip);
        _queueSources.Enqueue(source);
    }

    public void StopLoop(string name)
    {
        foreach (var sound in soundLoops)
        {
            if (sound.name.Equals(name))
            {
                sound.Stop();
            }
        }
    }
    public void PlayLoop(string name)
    {
        foreach (var sound in soundLoops)
        {
            if (sound.name.Equals(name))
            {
                sound.Play();
            }
        }
    }
}
