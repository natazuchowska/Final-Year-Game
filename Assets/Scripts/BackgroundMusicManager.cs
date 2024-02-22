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

    [SerializeField] private int sceneID;
    private int changeAudio;

    // Start is called before the first frame update
    void Start()
    {
        currentAudio = mainSceneAudio;

        currentAudio.Play();
        changeAudio = 0; // default audio
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DecideAudio();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecideAudio()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (currentAudio == null)
        {
            currentAudio = mainSceneAudio;
        }

        currentAudio.Pause();

        if (sceneID == 0)
        {
            currentAudio = mainSceneAudio;
            currentAudio.volume = 0.7f;
        }
        if (sceneID == 1 || sceneID == 8 || sceneID == 9 || sceneID == 12 || sceneID == 13) // round room and all puzzles inside of it
        {
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
            currentAudio = GHAudio;
            currentAudio.volume = 0.8f;
        }


        currentAudio.Play();
    }
}

