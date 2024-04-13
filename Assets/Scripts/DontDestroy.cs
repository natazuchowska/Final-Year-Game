using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class DontDestroy : MonoBehaviour
{
    private int sceneID;
   
    void Awake()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        AudioListener[] listeners = GameObject.FindObjectsByType<AudioListener>(FindObjectsSortMode.None);


        if (player.Length > 1) // destroy if any duplicates of the player occur
        {
            Destroy(this.gameObject); // avoid duplicates (if plaer already in the scene we don't want to keep the object from previous scene
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        if (listeners.Length > 1)
        {
            Destroy(this.gameObject); // avoid duplicates 
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }



}