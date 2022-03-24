using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Healer Object", menuName = "Items/Used/Healer")]
public class HealerObject : ItemObject
{
    // [Header("Healer")]
    [Tooltip("채력 회복률")]
    public int restoreHealth;
    public int restoreBattery;
    public void Awake()
    {
        itemType = ItemType.Used;
    }
}