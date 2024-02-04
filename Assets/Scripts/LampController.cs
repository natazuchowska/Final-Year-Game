using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    bool lightOn = true; // is light turned on(?)
    GameObject lightSprite;

    // Start is called before the first frame update
    void Start()
    {
        lightOn = ElectricitySnapController.lightOn; // get light info from electricity box
        lightSprite = GameObject.FindGameObjectWithTag("LampLight");
        // lightSprite.SetActive(true); // light urned on by default

        Debug.Log("lightOn value in swimming pool: " + lightOn);
    }

    private void Update()
    {
        lightSprite.SetActive(lightOn);

        /*if (lightOn == false)
        {
            Debug.Log("LIGHT TURNED OFF!!!!! FISH ARE SAVED");
            lightSprite.SetActive(false); // turn off light
            Debug.Log("lightOn value in swimming pool: " + lightOn);
        }*/
    }
}
