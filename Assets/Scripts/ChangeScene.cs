using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void ButtonClicked()
    {
        // Debug.Log("BUTTON CLICKED");
    }
    public void MoveToScene(int sceneID)
    {
        if(PauseController.isPaused) { return;  } // game paused so don't execute

        if(sceneID == -1) // this was implemented if in the future the user can go back to settings/controls after starting the game
        {
            if(GameObject.FindGameObjectWithTag("GameManager") != null)
            {
                SceneManager.LoadScene(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().previousSceneID); // get the id of previous scene
            }
            else
            {
                SceneManager.LoadScene(0); // go back to startscreen if game has not been started yet
            }
        }
        else
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}
