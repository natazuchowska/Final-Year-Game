/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "SFXVolume";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolume(); // load volume for the mixer
    }

    void LoadVolume() // load volume saved in 'VolumeSettings'
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.7f);

        // set the mixer channels to the loaded values
        mixer.SetFloat(MUSIC_KEY, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(SFX_KEY, Mathf.Log10(sfxVolume) * 20);
    }
}
