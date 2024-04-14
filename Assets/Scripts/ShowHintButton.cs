using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHintButton : MonoBehaviour
{
    [SerializeField] private AudioSource bookSound;
    [SerializeField] private AudioSource lockedSound;

    // unblock corresponding hints only at certain levels of the game
    public static bool[] hintActive;

    [SerializeField] List<GameObject> hints;

    [SerializeField] GameObject[] texts;

    public bool canvasOpen = false;

    [SerializeField] GameObject myHint;
    [SerializeField] GameObject myText;

    private void Start()
    {
        hintActive = HintManager.hintActive;
        hints = GameObject.Find("GameManager").GetComponent<HintManager>().hints;

        lockedSound = GameObject.Find("LockedSound").GetComponent<AudioSource>();
    }

    public void ShowHint(/*GameObject myHint*/)
    {
/*        myHint.SetActive(true);
        HideText(myText); // hide corresponding cover text
        bookSound.Play();*/

        if (hintActive[hints.IndexOf(myHint)] == true) // only show hint if unblocked already
        {
            myHint.SetActive(true);
            HideText(myText); // hide corresponding cover text
            bookSound.Play();
        }
        else
        {
            lockedSound.Play();
        }
    }

    public void HideText(GameObject text)
    {
        text.SetActive(false);
    }

}
