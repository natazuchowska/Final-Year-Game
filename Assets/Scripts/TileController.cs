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

    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved

    // Start is called before the first frame update
    void Start()
    {
        backgroundAfter = GameObject.FindGameObjectWithTag("BackgroundAfter"); // background to set after puzzle is solved
        backgroundAfter.SetActive(false);

        lightOn = ElectricitySnapController.lightOn; // get light info from electricity box

        tileButton.onClick.AddListener(TaskOnClick);

        keyReward = GameObject.FindGameObjectWithTag("KeyReward"); // get the reference to the correct object
        keyReward.SetActive(false); // hide the key by default
    }

    // todo when button clicked 
    private void TaskOnClick()
    {
        if(lightOn == false)
        {
            backgroundAfter.SetActive(true); // remove the tile
            keyReward.SetActive(true); // show the key
            audioPlayer.Play();
        }
    }
}
