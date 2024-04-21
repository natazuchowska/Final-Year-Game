using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishThankYouManager : MonoBehaviour
{
    [SerializeField] private GameObject bubble1;
    [SerializeField] private GameObject bubble2;

    [SerializeField] private GameObject dialogueCircle;
    [SerializeField] private GameObject character;

    private static bool hasBeenDisabled; // flag when dialogueCircle is setActive to false

    // Start is called before the first frame update
    void Start()
    {
        // get reference to the speech bubbles
        bubble1 = GameObject.Find("thankYou1");
        bubble2 = GameObject.Find("thankYou2");

        dialogueCircle = GameObject.Find("DialogueCircle");
        character = GameObject.Find("Character");

        // disable by default when light is on
        bubble1.SetActive(false);
        bubble2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if light turned off and bubbles not shown
        bool lightOn = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().lightOn;
        if (!bubble1.activeSelf && !bubble2.activeSelf)
        {
            if(!hasBeenDisabled && GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().checkIfSolved(2))
            {
                // display speech bubbles saying thank you
                bubble1.SetActive(true);
                bubble2.SetActive(true);

                Destroy(dialogueCircle); // disable the dialogue circle so that you can't talk to fish again
                Destroy(character); // destroy the character collider as the fish cannot be interacter with anymore

                hasBeenDisabled = true;
            }
        }
    }
}
