using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] AudioSource sfxAudio;
    private bool flag = false;
    // private bool flag2 = false;

    public const string MIXER_MUSIC = "musicVolume";
    public const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSoundsVolume);
        sfxSlider.onValueChanged.AddListener(delegate { PlaySound(); });
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 0.7f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 0.7f);
        flag = true;
    }

    public void PlaySound()
    {
        
        if (flag == true)
        {

            sfxAudio.Play();
            // StartCoroutine(WaitRoutine());
        }
    }

    IEnumerator WaitRoutine()
    {
        
        yield return new WaitForSeconds(2);
        sfxAudio.Pause();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);

        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

}
