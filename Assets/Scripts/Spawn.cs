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
        // Vector2 playerPos = new Vector2(player.position.x + 3, player.position.y - 2);
        // Instantiate(item, playerPos, Quaternion.identity);
        GameObject newInstance = Instantiate(item, new Vector3(0, 0, -9), Quaternion.identity); // instantiate item in the centre for now
        // newInstance.AddComponent<Draggable>(); // add draggable script to the newly instantiated object
    }
}
