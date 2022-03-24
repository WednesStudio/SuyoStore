using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Furniture Object", menuName = "Items/Furniture")]
public class FurnitureObject : ItemObject
{
    public void Awake()
    {
        itemType = ItemType.Furniture;
    }
}