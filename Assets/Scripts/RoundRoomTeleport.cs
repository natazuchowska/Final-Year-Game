using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.plantGiven == true) // only move to RR scene if plant given ==> door open
        {
            this.GetComponent<ChangeScene>().MoveToScene(1);
        }
    }
}
