/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;

public class LampController : MonoBehaviour
{
    public static bool lightOn; // is light turned on(?)
    GameObject lightSprite;

    // Start is called before the first frame update
    void Start()
    {
        lightOn = CalculatorLampPuzzle.lightOn; // get light info from electricity box
        lightSprite = GameObject.FindGameObjectWithTag("LampLight");

        Debug.Log("lightOn value in swimming pool: " + lightOn);
    }

    private void Update()
    {
        lightSprite.SetActive(lightOn);
    }
}
