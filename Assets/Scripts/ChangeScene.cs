using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        if(sceneID == -1)
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
