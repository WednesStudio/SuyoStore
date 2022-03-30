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
    private int currentWeight;
    public int CurrentWeight
    {
        get { return currentWeight; }
        set { currentWeight += value; }
    }
    public void Awake()
    {
        itemType = ItemType.Equipment;
    }
    public void onEquip()
    {
        // count weight
        // if weight over capacity affect speed
        // setSpeed()
    }
}
