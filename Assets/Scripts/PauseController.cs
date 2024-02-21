using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool isPaused; // check if the game is currently paused

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

                foreach(AudioSource a in audios)
                {
                    if (a.CompareTag("Music")) // only pause if it is the main scene music
                    {
                        a.Play();
                    }
                }
            }
            else
            {
                Time.timeScale = 0;
                Debug.Log("GAME PAUSED");

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
