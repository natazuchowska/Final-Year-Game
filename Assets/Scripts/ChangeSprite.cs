using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    /*public SpriteRenderer spriteRender;
    public Sprite disabledSprite; // item from slot cannot be used -> dark arrow
    public Sprite activeSprite; // item can be used -> green arrow*/

    int sceneID;
    [SerializeField] bool isActive;
    public GameObject icon1;
    public GameObject icon2;
    public GameObject icon3;
    public GameObject icon4;
    public GameObject icon5;
    public GameObject icon6;
    public GameObject icon7;
    public GameObject icon8;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
        // inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas

        icon1.SetActive(false);
        icon2.SetActive(false);
        icon3.SetActive(false);
        icon4.SetActive(false);
        icon5.SetActive(false);
        icon6.SetActive(false);
        icon7.SetActive(false);
        icon8.SetActive(false);
    }
    private void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
        isActive = this.gameObject.activeSelf;
        //inventoryOpen = inventoryCanvas.activeSelf;
        //correctScene = (sceneID == 1);


        if (sceneID == 1)
        {
            ChangeSpriteActive();
        }
        else
        {
            ChangeSpriteDisabled();
        }
    }

   /* private void OnMouseEnter()
    {
        Debug.Log("CHANGE ARROW NOW");
        ChangeSpriteActive();
    }

    private void OnMouseExit()
    {
        Debug.Log("CHANGE ARROW TO DISABLED BACK");
        ChangeSpriteDisabled();
    }*/

    void ChangeSpriteActive()
    {
        icon1.SetActive(true);
    }

    void ChangeSpriteDisabled()
    {
        icon1.SetActive(false);
    }
}
