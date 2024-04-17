using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRegister : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A key pressed");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W key pressed");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S key pressed");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D key pressed");
        }
    }
}
