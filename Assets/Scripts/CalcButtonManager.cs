using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalcButtonManager : MonoBehaviour
{
    Button btn;
    [SerializeField] public AudioSource clickAudio; // audio for clicking the button

    private void Awake()
    {
        /*btn.onClick.AddListener(RecordClick);*/
        btn = this.gameObject.GetComponent<Button>();
    }
    public void RecordClick()
    {
        Debug.Log("button " + this.gameObject.name + " clicked");
        GameObject.Find("CalcManager").GetComponent<CalculatorLampPuzzle>().setClickOrderID(btn);

        clickAudio.Play();
    }
}
