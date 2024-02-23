using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HintManager : MonoBehaviour
{
    [SerializeField] GameObject hintCanvas;

    [SerializeField] GameObject hint1;
    [SerializeField] GameObject hint2;
    [SerializeField] GameObject hint3;
    [SerializeField] GameObject hint4;

    Button btn1;
    Button btn2;
    Button btn3;
    Button btn4;

    [SerializeField] Button hintButton;
    bool canvasOpen;

    // Start is called before the first frame update
    void Start()
    {
        hintCanvas.SetActive(false); // hide canvas
        hintButton.onClick.AddListener(ShowCanvas); // display canvas when button clicked

        // store references to hint images
       /* hint1 = GameObject.Find("Hint1");
        hint2 = GameObject.Find("Hint2");
        hint3 = GameObject.Find("Hint3");
        hint4 = GameObject.Find("Hint4");*/

        /*btn1.onClick.AddListener(ShowHint);
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
        if(canvasOpen == false)
        {
            hintCanvas.SetActive(true);
        }
        else
        {
            hintCanvas.SetActive(false);
        }

        canvasOpen = !canvasOpen;
        
    }

 /*   // Update is called once per frame
    private void ShowHint()
    {
        hint1.SetActive(true);
        yield return null;
    }*/
}
