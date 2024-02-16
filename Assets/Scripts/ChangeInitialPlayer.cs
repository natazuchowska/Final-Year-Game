using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeInitialPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] int sceneID;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the index of current scene (vary the position/scale of the player based on this)

        // ROUND ROOM scene - put the player next to entrance on the left
        if (sceneID == 1)
        {
            // playerObj = this.gameObject;
            player.transform.localPosition = new Vector3(-3, -3, -3); // move player to (1, 1, 1) coordinates in the scene
        }
        // player.transform.localPosition = new Vector3(3.0f, 3.0f, 3.0f);
    }

}
