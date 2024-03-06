using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayerRender : MonoBehaviour
{
    private void OnEnable()
    {
        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().newGameStarted == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player")); // avoid duplicates
        }
    }
}
