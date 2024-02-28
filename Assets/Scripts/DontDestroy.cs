using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class DontDestroy : MonoBehaviour
{
   
    void Awake()
    {
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] mainMusic = GameObject.FindGameObjectsWithTag("Music");

        if (player.Length > 1) // destroy if any duplicates of the player occur
        {
            Destroy(this.gameObject); // avoid duplicates (if plaer already in the scene we don't want to keep the object from previous scene
        }

        /*if(mainMusic.Length > 1)
        {
            Destroy(this.gameObject); // avoid any duplicates of main audio source
        }*/

        DontDestroyOnLoad(this.gameObject);
        // this.gameObject.transform.localScale *= 0.5f;
    }
}