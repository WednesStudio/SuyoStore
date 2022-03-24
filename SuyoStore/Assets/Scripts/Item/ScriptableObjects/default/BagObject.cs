using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Bag Object", menuName = "Items/Equipment/Bag")]

public class BagObject : ItemObject
{
    // [Header("Bag")]
    [Tooltip("적재량")]
    public int capacity;

    public void Awake()
    {
        itemType = ItemType.Equipment;
    }

}
