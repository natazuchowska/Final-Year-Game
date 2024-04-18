using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioSource currentAudio;
    public AudioSource mainSceneAudio; // beginning scene
    public AudioSource RRAudio; // round room
    public AudioSource GHAudio; // glasshouse
    public AudioSource menuAudio; // start screen - menu

    [SerializeField] private int sceneID;

    private AudioSource newAudio;


    void Start()
    {
        currentAudio = menuAudio;
        currentAudio.Play();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        // currentAudio.Play();
        DecideAudio();
    }


    public void DecideAudio()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (currentAudio == null)
        {
            currentAudio = menuAudio;
        }

        currentAudio.Pause();

        if (sceneID == 3) // main scene
        {
            // newAudio = mainSceneAudio;
            currentAudio = mainSceneAudio;
            currentAudio.volume = 0.7f;
        }
        if (sceneID == 1 || sceneID == 8 || sceneID == 9 || sceneID == 12 || sceneID == 13) // round room and all puzzles inside of it
        {
            // newAudio = RRAudio;
            currentAudio = RRAudio;
            if (sceneID != 1)
            {
                currentAudio.volume = 0.1f; // lower the volume in puzzle scenes
            }
            else
            {
                currentAudio.volume = 0.4f;
            }
        }
        if (sceneID == 6 || sceneID == 10 || sceneID == 11) // glasshouse ground and top floor and bottles puzzle
        {
            // newAudio = GHAudio;
            currentAudio = GHAudio;
            currentAudio.volume = 0.8f;
        }

        if (sceneID == 0 || sceneID == 4 || sceneID == 5 || sceneID == 16)
        {
            // newAudio = menuAudio;
            currentAudio = menuAudio;
            currentAudio.volume = 0.8f;
        }

        /*if (newAudio != currentAudio)
        {
            currentAudio = newAudio;
        }*/

        currentAudio.Play();
    }
}

