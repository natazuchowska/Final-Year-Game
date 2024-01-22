using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
