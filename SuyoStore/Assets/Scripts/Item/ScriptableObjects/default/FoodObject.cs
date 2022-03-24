using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Items/Used/Food")]
public class FoodObject : ItemObject
{
    // [Header("Food")]
    [Tooltip("포만감 증가률")]
    public int restoreSatiety;
    public void Awake()
    {
        itemType = ItemType.Used;
    }
}