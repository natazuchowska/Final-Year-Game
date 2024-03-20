using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{

    int sceneID;
    [SerializeField] bool isActive;

    // arrow icons for inventory slots
    public GameObject[] useIcon = new GameObject[8];


    public GameObject slot1Item;


    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
        // inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas

        foreach(GameObject icon in useIcon)
        {
            icon.SetActive(false); // disable all the active icons by default;
        }
    }
    private void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
        isActive = this.gameObject.activeSelf;
        //inventoryOpen = inventoryCanvas.activeSelf;
        //correctScene = (sceneID == 1);

    }

    public void ChangeSpriteActive(int i)
    {
        useIcon[i].SetActive(true);
    }

    public void ChangeSpriteDisabled(int i)
    {
        useIcon[i].SetActive(false);
    }
}
