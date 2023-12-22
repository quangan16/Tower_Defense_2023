using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Image musicKnob;
    [SerializeField] private Image musicBack;
    [SerializeField] private Image soundKnob;
    [SerializeField] private Image soundBack;
    [SerializeField] private Image hapticKnob;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private bool onMusic;
    [SerializeField] private bool onSfx;
    [SerializeField] private AudioMixerGroup musicGroup;
    [SerializeField] private AudioMixerGroup soundGroup;
    [SerializeField] private AudioSource mainMusic;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            mainMusic.Play();
        }
       
        // musicToggle.isOn = DataPersist.onMusic;
        // soundToggle.isOn = DataPersist.onSfx;
        ToggleMusic();
        ToggleSound();
    }
    public void ToggleMusic()
    {
        DataPersist.onMusic = musicToggle.isOn;
        if (DataPersist.onMusic)
        {
            EnableMusic();
        }
        else
        {
            DisableMusic();
           
        }

        Save();
    }

    public void ToggleSound()
    {
        DataPersist.onSfx = soundToggle.isOn; 
        if (DataPersist.onSfx)
        {
            EnableSound();
            
        }
        else
        {
            DisableSound();
        }

        Save();
    }

    private void EnableMusic()
    {
        musicGroup.audioMixer.SetFloat("musicVolume", 0.0f);
        musicBack.enabled = true;
        musicKnob.rectTransform.anchoredPosition = Vector3.right * 45;
    }

    private void DisableMusic()
    {
        musicGroup.audioMixer.SetFloat("musicVolume", -80.0f);
        musicBack.enabled = false;
        musicKnob.rectTransform.anchoredPosition = Vector3.left * 45;
    }

    private void EnableSound()
    {
        soundGroup.audioMixer.SetFloat("soundVolume", 0.0f);
        soundBack.enabled = true;
        soundKnob.rectTransform.anchoredPosition = Vector3.right * 45;
    }

    private void DisableSound()
    {
        soundGroup.audioMixer.SetFloat("soundVolume", -80.0f);
        soundBack.enabled = false;
        soundKnob.rectTransform.anchoredPosition = Vector3.left * 45;
    }

    private void Save()
    {
        DataPersist.SaveData();
    }
}
