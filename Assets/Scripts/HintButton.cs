using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class HintButton : MonoBehaviour
{
    Button hintBtn;
    [SerializeField] GameObject hintCanvas;
    PauseController pauseControl;
    HintManager hintMngr;

    private void Start()
    {
        hintCanvas = GameObject.Find("HintCanvas");

        hintBtn = this.gameObject.GetComponent<Button>();
        hintBtn.onClick.AddListener(ShowCanvas);

        pauseControl = GameObject.Find("GameManager").GetComponent<PauseController>();
        hintBtn.onClick.AddListener(pauseControl.manageAppearanceHints); // pause the game on click

        hintMngr = GameObject.Find("GameManager").GetComponent<HintManager>(); ;
    }

    public void ShowCanvas()
    {
        hintMngr.ShowCanvas();
    }

}
