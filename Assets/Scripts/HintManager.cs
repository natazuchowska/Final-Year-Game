using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Runtime.CompilerServices;

public class HintManager : MonoBehaviour
{
    [SerializeField] GameObject hintCanvas;
    // [SerializeField] private Button closeButton;

    [SerializeField] private AudioSource bookSound;

    // unblock corresponding hints only at certain levels of the game
    public static bool[] hintActive = {false, false, false, false}; 
    // 0 -> paintings
    // 1 -> cables
    // 2 -> bottles
    // 3 -> fish

    [SerializeField] public List<GameObject> hints;

    GameObject hint1Cover;
    GameObject hint2Cover;
    GameObject hint3Cover;
    GameObject hint4Cover;

    [SerializeField] List<Button> showHintButtons;

    [SerializeField] GameObject[] texts;

    [SerializeField] Button hintButton;
    public bool canvasOpen = false;

    PauseController pauseControl;

    private void Awake()
    {
        hintCanvas.SetActive(false); // hide canvas
        pauseControl = GameObject.Find("GameManager").GetComponent<PauseController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hint1Cover = GameObject.Find("Hint1Cover");
        hint2Cover = GameObject.Find("Hint2Cover");
        hint3Cover = GameObject.Find("Hint3Cover");
        hint4Cover = GameObject.Find("Hint4Cover");

        hintButton.onClick.AddListener(ShowCanvas); // display canvas when button clicked

        // hide the hints by default
        foreach(GameObject hint in hints)
        {
            hint.SetActive(false);
        }
    }

    public void ShowCanvas()
    {
        // Debug.Log("SHOWING HINT CANVAS");
        pauseControl.manageAppearanceHints(); // pause the gae when hints displayed

        foreach (GameObject hint in hints)
        {
            hint.SetActive(false);
        }

        foreach(GameObject text in texts)
        {
            text.SetActive(true);
        }

        if (canvasOpen == false)
        {
            hintCanvas.SetActive(true);
        }
        else
        {
            hintCanvas.SetActive(false);
        }

        canvasOpen = !canvasOpen;   
    }
}
