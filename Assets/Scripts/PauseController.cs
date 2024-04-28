using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public static bool isPaused = false; // check if the game is currently paused
    [SerializeField] private GameObject pauseCanvas;

    GameObject musicManager;
    public BackgroundMusicManager musicScript; //==> UNCOMMENT LATER

    [SerializeField] Button menuButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button settingsButton;

    // [SerializeField] Button hintButton;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        musicManager = GameObject.Find("MainMusic"); // get reference to the object controlling music
        musicScript = musicManager.GetComponent<BackgroundMusicManager>(); // get reference to music script ==> UNCOMMENT LATER
        menuButton.onClick.AddListener(manageAppearance); // call manageAppearance() when button clicked

        resumeButton.onClick.AddListener(manageAppearance);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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

    public void manageAppearance()
    {
        isPaused = !isPaused;
        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        if (isPaused == false)
        {
            Time.timeScale = 1;
            //Debug.Log("GAME RUNNING");

            pauseCanvas.SetActive(false);

            foreach (AudioSource a in audios)
            {
                if (a.CompareTag("Music")) // only pause if it is the main scene music
                {
                    if (musicScript != null) // ==> UNCOMMENT LATER
                    {
                        musicScript.DecideAudio();
                    }
                }
            }
        }
        else
        {
            Time.timeScale = 0;
            //Debug.Log("GAME PAUSED");

            pauseCanvas.SetActive(true);

            foreach (AudioSource a in audios)
            {
                if (a.CompareTag("Music")) // only pause if it is the main scene music
                {
                    if (musicScript != null) // ==> UNCOMMENT LATER
                    {
                        a.Pause();
                    }
                }
            }
        }
    }

    public void manageAppearanceHints()
    {
        isPaused = !isPaused;
        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        if (isPaused == false)
        {
            Time.timeScale = 1;
            //Debug.Log("GAME RUNNING");

        }
        else
        {
            Time.timeScale = 0;
            //Debug.Log("GAME PAUSED");

        }
    }
}
