using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Object.DontDestroyOnLoad example.
//
// This script example manages the playing audio. The GameObject with the
// "music" tag is the BackgroundMusic GameObject. The AudioSource has the
// audio attached to the AudioClip.

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject); // avoid duplicates (if plaer already in the scene we don't want to keep the object from previous scene
        }

        DontDestroyOnLoad(this.gameObject);
        // this.gameObject.transform.localScale *= 0.5f;
    }
}