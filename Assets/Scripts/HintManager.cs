using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class HintManager : MonoBehaviour
{
    [SerializeField] GameObject hintCanvas;

    [SerializeField] GameObject hint1;
    [SerializeField] GameObject hint2;
    [SerializeField] GameObject hint3;
    [SerializeField] GameObject hint4;

    GameObject hint1Cover;
    GameObject hint2Cover;
    GameObject hint3Cover;
    GameObject hint4Cover;


    Button btn1;
    Button btn2;
    Button btn3;
    Button btn4;

    [SerializeField] GameObject text1;
    [SerializeField] GameObject text2;
    [SerializeField] GameObject text3;
    [SerializeField] GameObject text4;

    [SerializeField] Button hintButton;
    bool canvasOpen;

    // Start is called before the first frame update
    void Start()
    {
        hint1Cover = GameObject.Find("Hint1Cover");
        hint2Cover = GameObject.Find("Hint2Cover");
        hint3Cover = GameObject.Find("Hint3Cover");
        hint4Cover = GameObject.Find("Hint4Cover");

        // get the buttons of each hint card
        btn1 = hint1Cover.GetComponent<Button>();
        btn2 = hint2Cover.GetComponent<Button>();
        btn3 = hint3Cover.GetComponent<Button>();
        btn4 = hint4Cover.GetComponent<Button>();

        hintCanvas.SetActive(false); // hide canvas
        hintButton.onClick.AddListener(ShowCanvas); // display canvas when button clicked

/*      btn1.onClick.AddListener(ShowHint);
        btn2.onClick.AddListener(ShowHint);
        btn3.onClick.AddListener(ShowHint);
        btn4.onClick.AddListener(ShowHint);*/


        // hide the hints by default
        hint1.SetActive(false);
        hint2.SetActive(false);
        hint3.SetActive(false);
        hint4.SetActive(false);
    }

    void ShowCanvas()
    {
        hint1.SetActive(false);
        hint2.SetActive(false);
        hint3.SetActive(false);
        hint4.SetActive(false);

        text1.SetActive(true);
        text2.SetActive(true);
        text3.SetActive(true);
        text4.SetActive(true);

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


    public void ShowHint(GameObject hint)
    {
        hint.SetActive(true);
    }

    public void HideText(GameObject text)
    {
        text.SetActive(false); 
    }
}
