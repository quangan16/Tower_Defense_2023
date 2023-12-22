using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioSource[] soundSources;
    private Queue<AudioSource> _queueSources;

    public List<SoundArray> sfxs;
    public List<Sound> musics;

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
        DontDestroyOnLoad(this);
        OnInit();
    }
    public void OnInit()
    {

        //ChangeSoundVolume();
        //ChangeMusicVolume();

    }
    /*
        public void ChangeSoundVolume()
        {
            foreach (var sound in soundSources)
            {
                //sound.volume = UserData.FxSound;
                sound.mute = !UserData.IsSoundOn;

            }
            foreach (var sound in soundLoops)
            {
                //sound.volume = UserData.FxSound;
                sound.mute = !UserData.IsSoundOn;
            }
        }*/

/*    public void ChangeMusicVolume()
    {
        if (musicSource != null)
        {
            musicSource.mute = !UserData.IsSoundOn;
            //musicSource.volume = UserData.Music;
        }
    }*/

    public void PlayShot(string name, int index)
    {
        SoundArray sounds = sfxs.Find(x => x.name == name);
        if (sounds == null)
        {
            return;
        }
        var source = _queueSources.Dequeue();
        if (!source)
        {
            return;
        }
        source.PlayOneShot(sounds.Sounds[index].clip);
        _queueSources.Enqueue(source);
    }
}