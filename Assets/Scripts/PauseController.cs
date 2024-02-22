using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public bool isPaused = false; // check if the game is currently paused
    [SerializeField] private GameObject pauseCanvas;

    GameObject musicManager;
    public BackgroundMusicManager musicScript;

    [SerializeField] Button menuButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button settingsButton;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        musicManager = GameObject.Find("MainMusic"); // get reference to the object controlling music
        musicScript = musicManager.GetComponent<BackgroundMusicManager>(); // get reference to music script
        menuButton.onClick.AddListener(manageAppearance); // call manageAppearance() when button clicked

        // clicking ANY of the three buttons should close the prompt
        resumeButton.onClick.AddListener(manageAppearance);
        // mainMenuButton.onClick.AddListener(manageAppearance);
        // settingsButton.onClick.AddListener(manageAppearance);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc key 
        {
            manageAppearance();
        }   
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isPaused = true;
        manageAppearance();
    }

    void manageAppearance()
    {
        isPaused = !isPaused;
        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        if (isPaused == false)
        {
            Time.timeScale = 1;
            Debug.Log("GAME RUNNING");

            pauseCanvas.SetActive(false);

            foreach (AudioSource a in audios)
            {
                if (a.CompareTag("Music")) // only pause if it is the main scene music
                {
                    if(musicScript != null)
                    {
                        musicScript.DecideAudio();
                    }
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
                if (a.CompareTag("Music")) // only pause if it is the main scene music
                {
                    if(musicScript != null)
                    {
                        a.Pause();
                    }
                }
            }
        }
    }
}
