using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Light Object", menuName = "Items/Equipment/Light")]

public class LightObject : ItemObject
{
    // [Header("Light")]
    [Tooltip("시야")]
    public int sightRange;

    public void Awake()
    {
        itemType = ItemType.Equipment;
        attributes[(int)Attributes.SightRange] = sightRange;
    }
    public void OnValidate()
    {
        attributes[(int)Attributes.SightRange] = sightRange;
    }
}
