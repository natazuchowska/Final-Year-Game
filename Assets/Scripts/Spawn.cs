using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;

    private void Start()
    {
       //  player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnDroppedItem()
    {
        GameObject newInstance = Instantiate(item, new Vector3(-7, 0, -9), Quaternion.identity); // instantiate item in the centre for now
    }
}
