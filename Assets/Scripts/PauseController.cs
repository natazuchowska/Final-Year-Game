using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool isPaused; // check if the game is currently paused
    [SerializeField] private GameObject pauseCanvas;

    GameObject musicManager;
    public BackgroundMusicManager musicScript;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        musicManager = GameObject.Find("MainMusic"); // get reference to the object controlling music
        musicScript = musicManager.GetComponent<BackgroundMusicManager>(); // get reference to music script
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // esc key
        {
            isPaused = !isPaused;
            AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

            if (isPaused == false)
            {
                Time.timeScale = 1;
                Debug.Log("GAME RUNNING");

                pauseCanvas.SetActive(false);

                foreach(AudioSource a in audios)
                {
                    if (a.CompareTag("Music")) // only pause if it is the main scene music
                    {
                        musicScript.DecideAudio();
                    }
                }
            }
            else
            {
                Time.timeScale = 0;
                Debug.Log("GAME PAUSED");

                pauseCanvas.SetActive(true);

                foreach (AudioSource a in audios)
                {
                    if(a.CompareTag("Music")) // only pause if it is the main scene music
                    {
                        a.Pause();
                    }
                }
            }
        }
    }
}
