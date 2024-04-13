using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRoomTeleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.GetComponent<ChangeScene>().MoveToScene(1);
    }
}
