using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    GameObject backgroundAfter; 
    bool lightOn;
    [SerializeField] public Button tileButton;
    GameObject keyReward; // key to get as a reward for turning light off -> need to click the tile to reveal
    public static bool keyCollected; // has key been picked up already?

    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved


    void Start()
    {
        backgroundAfter = GameObject.FindGameObjectWithTag("BackgroundAfter"); // background to set after puzzle is solved
        backgroundAfter.SetActive(false);

        lightOn = CalculatorLampPuzzle.lightOn; // get light info from electricity box

        tileButton.onClick.AddListener(TaskOnClick);

        keyReward = GameObject.FindGameObjectWithTag("KeyReward"); // get the reference to the correct object
        keyReward.SetActive(false); // hide the key by default

        if(keyCollected)
        {
            backgroundAfter.SetActive(true); // key was collected prev. so show dislocated tile
        }
    }

    // todo when button clicked 
    private void TaskOnClick()
    {
        if(lightOn == false && keyCollected == false) // only if key not collected previously
        {
            backgroundAfter.SetActive(true); // remove the tile
            keyReward.SetActive(true); // show the key
            keyCollected = true;
            audioPlayer.Play();
        }
    }
}
