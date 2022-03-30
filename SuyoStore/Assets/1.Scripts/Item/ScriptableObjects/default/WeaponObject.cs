using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Types;

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Items/Equipment/Weapon")]

public class WeaponObject : ItemObject
{
    // [Header("Weapon")]
    [Tooltip("공격력")]
    public int attackBonus;

    public void Awake()
    {
        itemType = ItemType.Equipment;
        attributes[(int)Attributes.Attack] = attackBonus;
    }
    public void OnValidate()
    {
        attributes[(int)Attributes.Attack] = attackBonus;
    }
    public void onEquip()
    {
        // UseItem(this);
    }
}
