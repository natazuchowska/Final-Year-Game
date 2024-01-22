using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public bool[] isFull; // to check if a certain slot is already full
    public GameObject[] slots; // where to add a particular item
}
