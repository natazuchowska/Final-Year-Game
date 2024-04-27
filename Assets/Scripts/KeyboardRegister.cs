using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardRegister : MonoBehaviour
{

    // for build versions debugging purposes only
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

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("A key released");
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("W key released");
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("S key released");
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("D key released");
        }
    }
}
